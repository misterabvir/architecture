
using MassTransit;

using RobotCloudService.Notifications;
using RobotCloudService.Web.Components.Remotes.Services;

namespace RobotCloudService.Web.Components.Remotes.Consumers;

internal class CleaningStoppedConsumer(INotificationService notificationService) : IConsumer<CleanStoppedEvent>
{

    public async Task Consume(ConsumeContext<CleanStoppedEvent> context)
    {
        await notificationService.NotifyMessageReceived(context.Message.Message);
    }
}
