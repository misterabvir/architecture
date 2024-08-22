using System.Text.Json;
using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CloudService.Infrastructure.Repositories;

public class DeviceCacheRepository(DeviceRepository decorated, IDistributedCache cache) : IDeviceRepository
{
    public async Task<Cpu?> GetCpuById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetOrCreate($"cpu-{id}", () => decorated.GetCpuById(id, cancellationToken), cancellationToken);
    }

    public async Task<List<Cpu>> GetCpus(CancellationToken cancellationToken = default)
    {
        return (await GetOrCreate("cpus", () => decorated.GetCpus(cancellationToken), cancellationToken))!;
    }

    public async Task<Ip?> GetIpById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetOrCreate($"ip-{id}", () => decorated.GetIpById(id, cancellationToken), cancellationToken);
    }

    public async Task<List<Ip>> GetIps(CancellationToken cancellationToken = default)
    {
        return (await GetOrCreate("ips", () => decorated.GetIps(cancellationToken), cancellationToken))!;
    }    

    public async Task<Os?> GetOsById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetOrCreate($"os-{id}", () => decorated.GetOsById(id, cancellationToken), cancellationToken);
    }

    public async Task<List<Os>> GetOss(CancellationToken cancellationToken = default)
    {
        return (await GetOrCreate("oss", () => decorated.GetOss(cancellationToken), cancellationToken))!;
    }

    public async Task<Ram?> GetRamById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetOrCreate($"ram-{id}", () => decorated.GetRamById(id, cancellationToken), cancellationToken);
    }

    public async Task<List<Ram>> GetRams(CancellationToken cancellationToken = default)
    {
        return (await GetOrCreate("rams", () => decorated.GetRams(cancellationToken), cancellationToken))!;
    }

    public async Task<Rom?> GetRomById(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetOrCreate($"rom-{id}", () => decorated.GetRomById(id, cancellationToken), cancellationToken);
    }

    public async Task<List<Rom>> GetRoms(CancellationToken cancellationToken = default)
    {
        return (await GetOrCreate("roms", () => decorated.GetRoms(cancellationToken), cancellationToken))!;
    }  

    private async Task<T?> GetOrCreate<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken)
    {
        var json = await cache.GetStringAsync(key, cancellationToken);
        if (json is not null)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        var result = await factory();
        if (result != null)
        {
            await cache.SetStringAsync(key, JsonSerializer.Serialize(result), token: cancellationToken);
        }

        return result;
    }
}