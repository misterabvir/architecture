using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using CloudService.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace CloudService.Infrastructure.Repositories;

public class DeviceCacheRepository(DeviceRepository decorated, IDistributedCache cache) : IDeviceRepository
{
    private const int _expire = 3000;

    public async Task<Cpu?> GetCpuById(Guid id, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate(
            key: $"cpu-{id}",
            factory: () => decorated.GetCpuById(id, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<List<Cpu>> GetCpus(CancellationToken cancellationToken = default) 
        => (await cache.GetOrCreate(
            key: "cpus",
            factory: () => decorated.GetCpus(cancellationToken)!,
            expiration: _expire,
            cancellationToken: cancellationToken))!;

    public async Task<Ip?> GetIpById(Guid id, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate(
            key: $"ip-{id}",
            factory: () => decorated.GetIpById(id, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<List<Ip>> GetIps(CancellationToken cancellationToken = default) 
        => (await cache.GetOrCreate(
            key: "ips",
            factory: () => decorated.GetIps(cancellationToken)!,
            expiration: _expire,
            cancellationToken: cancellationToken))!;

    public async Task<Os?> GetOsById(Guid id, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate(
            key: $"os-{id}",
            factory: () => decorated.GetOsById(id, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<List<Os>> GetOss(CancellationToken cancellationToken = default) 
        => (await cache.GetOrCreate(
            key: "oss",
            factory: () => decorated.GetOss(cancellationToken)!,
            expiration: _expire,
            cancellationToken: cancellationToken))!;

    public async Task<Ram?> GetRamById(Guid id, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate(
            key: $"ram-{id}",
            factory: () => decorated.GetRamById(id, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<List<Ram>> GetRams(CancellationToken cancellationToken = default) 
        => (await cache.GetOrCreate(
            key: "rams",
            factory: () => decorated.GetRams(cancellationToken)!,
            expiration: _expire,
            cancellationToken: cancellationToken))!;

    public async Task<Rom?> GetRomById(Guid id, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate(
            key: $"rom-{id}",
            factory: () => decorated.GetRomById(id, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<List<Rom>> GetRoms(CancellationToken cancellationToken = default) 
        => (await cache.GetOrCreate(
            key: "roms",
            factory: () => decorated.GetRoms(cancellationToken)!,
            expiration: _expire,
            cancellationToken: cancellationToken))!;

}