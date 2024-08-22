using CloudService.Domain;

namespace CloudService.Application.Base.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId, bool isTrack = false, bool includeOrderDetails = false, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task ClearCacheAsync(CancellationToken cancellationToken = default);
}