namespace RemoteCleaner.Server.Infrastructure.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IStationRepository Stations { get; }
    IRoomRepository Rooms { get; }
 }
