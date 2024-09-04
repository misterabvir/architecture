using System.Text.Json;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using RobotCloudService.Application.Common;
using RobotCloudService.Notifications;

namespace RobotCloudService.Remotes.Application.Users.Events;

public static class CleanStarted
{
    public record Notification(Ulid UserId, string Message, Ulid RobotId, Ulid RoomId, DateTime OccurredOn, DateTime CalculatedFinishTime) : IDomainEvent;
    public class Handler(IPublishEndpoint endpoint, IDistributedCache cache) : INotificationHandler<Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            var queueEvent = new CleanStartedEvent(notification.UserId, notification.Message, notification.RobotId, notification.RoomId, notification.OccurredOn);
            await endpoint.Publish(queueEvent, cancellationToken);

            var json = await cache.GetStringAsync("jobs", cancellationToken);
            List<Notification>? jobs = null;
            if (json is not null)
            {
                jobs = JsonSerializer.Deserialize<List<Notification>>(json);
            }
            jobs ??= [];
            jobs.Add(notification);
            await cache.SetStringAsync("jobs", JsonSerializer.Serialize(jobs), cancellationToken);
        }
    }
}
