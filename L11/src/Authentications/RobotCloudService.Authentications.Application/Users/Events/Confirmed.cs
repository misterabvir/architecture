using MassTransit;

using MediatR;

using RobotCloudService.Application.Common;
using RobotCloudService.Notifications;

namespace RobotCloudService.Authentications.Application.Users.Events;

public static class Confirmed
{
    public record Notification(Ulid UserId, string Email) : IDomainEvent;

    public class Handler(IPublishEndpoint endpoint) : INotificationHandler<Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            var queueEvent = new UserRegisteredEvent(notification.UserId, DateTime.UtcNow);
            await endpoint.Publish(queueEvent, cancellationToken);
        }
    }
}




