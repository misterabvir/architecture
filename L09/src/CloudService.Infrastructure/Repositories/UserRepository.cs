using CloudService.Application.Base.Repositories;
using CloudService.Domain;
using CloudService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CloudService.Infrastructure.Repositories;

internal class UserRepository(CloudServiceDbContext context) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken: cancellationToken);
    }

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return context.Users.AsNoTracking().AnyAsync(u => u.Username == username, cancellationToken: cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid userId, bool isTrack = false, bool includeOrderDetails = false, CancellationToken cancellationToken = default)
    {
        return await  context.Users.AsNoTracking().Where(u => u.UserId == userId).Include(u => u.Setups).ThenInclude(o => o.Os)
            .Include(u => u.Setups).ThenInclude(o => o.Ip)
            .Include(u => u.Setups).ThenInclude(o => o.Ram)
            .Include(u => u.Setups).ThenInclude(o => o.Rom)
            .Include(u => u.Setups).ThenInclude(o => o.Cpu)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);;

    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var query = context.Users.Where(u => u.Username == username);
        return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        await Task.CompletedTask;
    }
}