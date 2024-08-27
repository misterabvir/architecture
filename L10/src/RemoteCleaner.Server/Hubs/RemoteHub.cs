using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Application;
using RemoteCleaner.Server.Domain;
using RemoteCleaner.Server.Infrastructure.Persistence;
using System;
using System.Runtime.InteropServices;

namespace RemoteCleaner.Server.Hubs;

public partial class RemoteHub(RemoteCleanerDbContext context, IRobotRemote remote) : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        await Clients.All.SendAsync("Battery", remote.ChargeValue);
    }


    public async Task Start(int roomId)
    {
        remote.OnProgressChanged += ProgressChanged;
        remote.OnChargeChanged += ChargeChanged;       

        var station = await context.Stations.FirstAsync();

        var room = await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId);
        if (room is null)
            return;

        await Clients.All.SendAsync("Started");

        station.Logs.Add(new Log()
        {
            Message = $"Going to {room.Name} from base",
            Time = DateTime.UtcNow
        });

        await remote.MoveASync(GetDistance(station!, room), $"Going into {room.Name}");

        station.Logs.Add(new Log()
        {
            Message = $"Started cleaning {room.Name}",
            Time = DateTime.UtcNow
        });

        await remote.CleanAsync(room.Width * room.Length, $"Cleaning {room.Name}");

        station.Logs.Add(new Log()
        {
            Message = $"Finished cleaning {room.Name}",
            Time = DateTime.UtcNow
        });

        await remote.MoveASync(GetDistance(room, station!), "Going to base");
        
        station.Logs.Add(new Log()
        {
            Message = $"Back to base from {room.Name}",
            Time = DateTime.UtcNow
        });

        await remote.Charging("Charging");

        station.Logs.Add(new Log()
        {
            Message = $"Charged",
            Time = DateTime.UtcNow
        });

        room.CleanedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();

        await Clients.All.SendAsync("Completed");

        remote.OnProgressChanged -= ProgressChanged;
        remote.OnChargeChanged -= ChargeChanged;
    }

    private void ChargeChanged(double charge)
    {
        Clients.All.SendAsync("Battery", charge).Wait();
    }


    private void ProgressChanged(double progress, string message)
    {

        Clients.All.SendAsync("Progress", message, progress).Wait();

    }

    private static double GetDistance(ILocation start, ILocation target)
    {
        var dx = start.X - target.X;
        var dy = target.X - target.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
