using RemoteCleaner.Server.Domain;

namespace RemoteCleaner.Server.Infrastructure.Repositories;

public interface IStationRepository
{
    Task<Station> GetStationAsync(CancellationToken cancellationToken = default);
    Task<List<Log>> GertLogsAsync(CancellationToken cancellationToken = default);
}
