using MassTransit;

using RobotCloudService.Notifications;
using RobotCloudService.Web.Components.Remotes.Services;

namespace RobotCloudService.Web.Components.Remotes.Consumers;

internal class CleaningStartedConsumer(INotificationService notificationService) : IConsumer<CleanStartedEvent>
{
    
    public async Task Consume(ConsumeContext<CleanStartedEvent> context)
    {
        await notificationService.NotifyMessageReceived(context.Message.Message);
    }
}
