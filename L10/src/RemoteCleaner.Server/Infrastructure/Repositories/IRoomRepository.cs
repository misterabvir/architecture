using RemoteCleaner.Server.Domain;

namespace RemoteCleaner.Server.Infrastructure.Repositories;

public interface IRoomRepository
{
   Task<List<Room>> GetRoomsAsync(CancellationToken cancellationToken = default);
    Task<Room?> GetRoomByIdAsync(int roomId, CancellationToken cancellationToken = default);
}
