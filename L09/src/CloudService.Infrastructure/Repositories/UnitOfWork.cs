using CloudService.Application.Base.Repositories;
using CloudService.Infrastructure.Contexts;

namespace CloudService.Infrastructure.Repositories;

internal class UnitOfWork(
    CloudServiceDbContext context, 
    IUserRepository userRepository,
    IDeviceRepository deviceRepository) : IUnitOfWork
{
    public IUserRepository Users => userRepository;
    public IDeviceRepository Devices => deviceRepository;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
