using Microsoft.EntityFrameworkCore;
using RemoteCleaner.Server.Domain;
using RemoteCleaner.Server.Infrastructure.Persistence;

namespace RemoteCleaner.Server.Infrastructure.Repositories;

public class StationRepository(RemoteCleanerDbContext context) : IStationRepository
{
    public async Task<List<Log>> GertLogsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Stations.AsNoTracking().SelectMany(r => r.Logs).OrderByDescending(s=>s.Time).ToListAsync(cancellationToken);
    }

    public async Task<Station> GetStationAsync(CancellationToken cancellationToken = default)
    {
        return await context.Stations.FirstAsync(cancellationToken);
    }
}
