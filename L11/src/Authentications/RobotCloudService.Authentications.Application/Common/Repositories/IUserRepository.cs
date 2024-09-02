using RobotCloudService.Authentications.Application.Users;

namespace RobotCloudService.Authentications.Application.Common.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> IsEmailExist(string email, CancellationToken cancellationToken = default);
}
