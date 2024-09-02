using MediatR;

using RobotCloudService.Authentications.Application.Common.Repositories;
using RobotCloudService.Authentications.Infrastructure.Persistence;
using RobotCloudService.Application.Common;

namespace RobotCloudService.Authentications.Infrastructure.Repositories
{
    internal class UnitOfWork(
        AuthenticationDbContext context, 
        IPublisher publisher,
        IUserRepository userRepository)  : IUnitOfWork
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
            
            if(rowAffected > 0)
            {
                foreach (var @event in events)
                {
                    await publisher.Publish(@event, cancellationToken);
                }
            }
            return rowAffected;
        }
    }
}
