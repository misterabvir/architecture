namespace RobotCloudService.Web.Components.Remotes.Services;

internal class NotificationService : INotificationService
{
    public event Action<string>? OnMessageReceived;
    public async Task NotifyMessageReceived(string message)
    {
        await Task.CompletedTask;
        OnMessageReceived?.Invoke(message);
    }

    
}