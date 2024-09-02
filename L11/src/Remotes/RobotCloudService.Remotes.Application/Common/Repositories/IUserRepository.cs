

using RobotCloudService.Remotes.Application.Users;

namespace RobotCloudService.Remotes.Application.Common.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken);
}