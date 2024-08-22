using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CloudService.Infrastructure.Extensions;

internal static class CacheExtensions
{
    private const int DefaultExpirationInMinutes = 15;


    internal static async Task<T?> GetOrCreate<T>(
        this IDistributedCache cache,
        string key,
        Func<Task<T?>> factory,
        int expiration = DefaultExpirationInMinutes,
        CancellationToken cancellationToken = default)
    {
        T? result = default;

        var json = await cache.GetStringAsync(key, cancellationToken);
        if (json is not null)
        {
            result = JsonSerializer.Deserialize<T>(json);
            if (result is not null)
            {
                return result;
            }
        }
        result = await factory();
        if (result != null)
        {
            await cache.SetStringAsync(
                key: key,
                value: JsonSerializer.Serialize(result),
                options: new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expiration) },
                token: cancellationToken);
        }

        return result;
    }
}
