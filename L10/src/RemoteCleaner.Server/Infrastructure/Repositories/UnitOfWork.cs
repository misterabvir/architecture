using RemoteCleaner.Server.Infrastructure.Persistence;

namespace RemoteCleaner.Server.Infrastructure.Repositories;



public class UnitOfWork(RemoteCleanerDbContext context, IStationRepository stations, IRoomRepository rooms) : IUnitOfWork
{
    public IStationRepository Stations => stations;
    public IRoomRepository Rooms => rooms;
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
