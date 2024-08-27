using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Domain;
using RemoteCleaner.Server.Infrastructure.Persistence;

namespace RemoteCleaner.Server.Infrastructure.Repositories;

public class RoomRepository(RemoteCleanerDbContext context) : IRoomRepository
{
    public async Task<Room?> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default)
    {
        return await context.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId, cancellationToken);
    }

    public async Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Rooms.AsNoTracking().ToListAsync(cancellationToken);
    }
}