using CloudService.Domain;
using CloudService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudService.Infrastructure.Repositories;

internal class UserRepository(CloudServiceDbContext context)
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken: cancellationToken);
    }

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return context.Users.AnyAsync(u => u.Username == username, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid userId, bool isTrack = false, bool includeOrderDetails = false, CancellationToken cancellationToken = default)
    {
        return await  context.Users.Where(u => u.UserId == userId).Include(u => u.Configs).ThenInclude(o => o.Os)
            .Include(u => u.Configs).ThenInclude(o => o.Ip)
            .Include(u => u.Configs).ThenInclude(o => o.Ram)
            .Include(u => u.Configs).ThenInclude(o => o.Rom)
            .Include(u => u.Configs).ThenInclude(o => o.Cpu)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);;

    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var query = context.Users.Where(u => u.Username == username);
        return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}