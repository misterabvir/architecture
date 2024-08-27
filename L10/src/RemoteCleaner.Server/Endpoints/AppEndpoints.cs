using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Infrastructure.Persistence;

namespace RemoteCleaner.Server.Endpoints;

public static class AppEndpoints
{
    public static void MapAppEndpoint(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/log", async (RemoteCleanerDbContext context) =>
        {
            return Results.Ok(await context.Stations.AsNoTracking().SelectMany(s => s.Logs).OrderByDescending(l => l.Time).ToListAsync());
        });

        routes.MapGet("/rooms", async (RemoteCleanerDbContext context) =>
        {
            return Results.Ok(await context.Rooms.AsNoTracking().ToListAsync());
        });
        routes.MapGet("station", async (RemoteCleanerDbContext context) =>
        {
            return Results.Ok(await context.Stations.AsNoTracking().FirstAsync());
        });
    }

}
