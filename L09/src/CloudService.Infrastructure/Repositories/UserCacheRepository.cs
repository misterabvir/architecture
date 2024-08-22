using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using CloudService.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace CloudService.Infrastructure.Repositories;

internal class UserCacheRepository(UserRepository decorated, IDistributedCache cache) : IUserRepository
{
    private const int _expire = 5;
    
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync($"user-{user.Username}-exist", cancellationToken);
        await decorated.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync($"user-{user.Username}-exist", cancellationToken);
        await cache.RemoveAsync($"user-{user.Username}-instance", cancellationToken);
        await cache.RemoveAsync($"user-{user.UserId}-instance", cancellationToken);
        await decorated.UpdateAsync(user, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate<bool>(
            key: $"user-{username}-exist",
            factory: () => decorated.ExistsByUsernameAsync(username, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default) 
        => await cache.GetOrCreate<User>(
            key: $"user-{userId}-instance",
            factory: () => decorated.GetByIdAsync(userId, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await cache.GetOrCreate<User>(
            key: $"user-{username}-instance",
            factory: () => decorated.GetByUsernameAsync(username, cancellationToken),
            expiration: _expire,
            cancellationToken: cancellationToken);
    }
}