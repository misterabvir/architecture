
using MassTransit;

using MediatR;

using RobotCloudService.Application.Common;
using RobotCloudService.Notifications;

namespace RobotCloudService.Remotes.Application.Users.Events;

internal static class CleanStarted
{
    public record Notification(Ulid UserId, string Message, Ulid RobotId, Ulid RoomId, DateTime OccurredOn) : IDomainEvent;
    public class Handler(IPublishEndpoint endpoint) : INotificationHandler<Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            var queueEvent = new CleanStartedEvent(notification.UserId, notification.Message, notification.RobotId, notification.RoomId, notification.OccurredOn);
            await endpoint.Publish(queueEvent, cancellationToken);
        }
    }
}
