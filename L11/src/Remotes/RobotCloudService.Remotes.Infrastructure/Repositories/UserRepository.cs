using Microsoft.EntityFrameworkCore;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users;
using RobotCloudService.Remotes.Infrastructure.Persistence;

namespace RobotCloudService.Remotes.Infrastructure.Repositories;

internal class UserRepository(RemoteDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);  
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.UserId == userId.ToString(), cancellationToken);
    }

}
