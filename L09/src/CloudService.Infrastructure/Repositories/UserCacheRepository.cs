using System.Text.Json;
using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using Microsoft.Extensions.Caching.Distributed;

namespace CloudService.Infrastructure.Repositories;

internal class UserCacheRepository(UserRepository decorated, IDistributedCache cache) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync("users_keys", cancellationToken);
        await decorated.AddAsync(user, cancellationToken);
    }

    public async Task ClearCacheAsync(CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync("users_keys", cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        string key = $"user-{username}-exist";
        var json = await cache.GetStringAsync(key, cancellationToken);
        if (json is not null)
        {
            return JsonSerializer.Deserialize<bool>(key);
        }
        var result = await decorated.ExistsByUsernameAsync(username, cancellationToken);

        await cache.SetStringAsync(key, result.ToString(), cancellationToken);
        await AddToKeysAsync(key, cancellationToken);

        return result;
    }



    public async Task<User?> GetByIdAsync(Guid userId, bool isTrack = false, bool includeOrderDetails = false, CancellationToken cancellationToken = default)
    {
        return await decorated.GetByIdAsync(userId, isTrack, includeOrderDetails, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        string key = $"user-{username}-instance";
        User? user= null;
        var json = await cache.GetStringAsync(key, cancellationToken);
        if(json is not null)
        {
            user = JsonSerializer.Deserialize<User>(json);
            if(user is not null)
            {
                return user;
            }
        }
        
        user = await decorated.GetByUsernameAsync(username, cancellationToken);

        if(user is not null)
        {
            await cache.SetStringAsync(key, JsonSerializer.Serialize(user), cancellationToken);
            await AddToKeysAsync(key, cancellationToken);
        }

        return  user;
    }

    private async Task AddToKeysAsync(string key, CancellationToken cancellationToken)
    {
        var json = await cache.GetStringAsync("users_keys", cancellationToken);
        var keys = json is null ? [] : JsonSerializer.Deserialize<List<string>>(json) ?? [];
        keys.Add(key);
        await cache.SetStringAsync("users_keys", JsonSerializer.Serialize(keys), cancellationToken);
    }
}