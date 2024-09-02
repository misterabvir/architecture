using MassTransit;

using RobotCloudService.Notifications;
using RobotCloudService.Remotes.Application.Common.Repositories;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Consumers
{
    internal class UserCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<UserRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            User user = User.Create(UserId.Create(context.Message.UserId));
            await unitOfWork.Users.AddAsync(user, context.CancellationToken);
            await unitOfWork.SaveChangesAsync(context.CancellationToken);
        }
    }

}
