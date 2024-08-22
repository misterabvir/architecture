using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using CloudService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudService.Infrastructure.Repositories;

public class DeviceRepository(CloudServiceDbContext context) : IDeviceRepository
{
    public async Task<Cpu?> GetCpuById(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Cpus.AsNoTracking().FirstOrDefaultAsync(c => c.CpuId == id, cancellationToken);
    }

    public async Task<List<Cpu>> GetCpus(CancellationToken cancellationToken = default)
    {
        return await context.Cpus.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Ip?> GetIpById(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Ips.AsNoTracking().FirstOrDefaultAsync(c => c.IpId == id, cancellationToken);
    }

    public async Task<List<Ip>> GetIps(CancellationToken cancellationToken = default)
    {
        return await context.Ips.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Os?> GetOsById(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Oss.AsNoTracking().FirstOrDefaultAsync(c => c.OsId == id, cancellationToken);
    }

    public async Task<List<Os>> GetOss(CancellationToken cancellationToken = default)
    {
        return await context.Oss.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Ram?> GetRamById(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Rams.AsNoTracking().FirstOrDefaultAsync(c => c.RamId == id, cancellationToken);
    }

    public async Task<List<Ram>> GetRams(CancellationToken cancellationToken = default)
    {
        return await context.Rams.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Rom?> GetRomById(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Roms.AsNoTracking().FirstOrDefaultAsync(c => c.RomId == id, cancellationToken);
    }

    public async Task<List<Rom>> GetRoms(CancellationToken cancellationToken = default)
    {
        return await context.Roms.AsNoTracking().ToListAsync(cancellationToken);
    }  
}