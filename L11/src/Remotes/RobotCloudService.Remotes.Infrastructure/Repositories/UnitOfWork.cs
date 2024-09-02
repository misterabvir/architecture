
using MediatR;

using RobotCloudService.Application.Common;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Infrastructure.Persistence;

namespace RobotCloudService.Remotes.Infrastructure.Repositories;

internal class UnitOfWork(RemoteDbContext context, IPublisher publisher, IUserRepository userRepository) : IUnitOfWork
{
    public IUserRepository Users => userRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var events = new List<IDomainEvent>();

        context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList()
            .ForEach(entity =>
            {
                events.AddRange(entity.DomainEvents);
                entity.ClearDomainEvents();
            });

        var rowAffected = await context.SaveChangesAsync(cancellationToken);

        if (rowAffected > 0)
        {
            foreach (var @event in events)
            {
                await publisher.Publish(@event, cancellationToken);
            }
        }
        return rowAffected;
    }
}
