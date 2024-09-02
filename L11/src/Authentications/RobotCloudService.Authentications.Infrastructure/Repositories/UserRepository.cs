using Microsoft.EntityFrameworkCore;
using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Application.Users;
using RobotCloudService.Authentications.Infrastructure.Persistence;

namespace RobotCloudService.Authentications.Infrastructure.Repositories;

internal class UserRepository(AuthenticationDbContext context) : IUserRepository
{

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        email = email.ToLowerInvariant();
        return context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken = default)
    {
        email = email.ToLowerInvariant();
        return await context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
    }


    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        context.Users.Update(user);
    }
}
