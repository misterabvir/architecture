
namespace RobotCloudService.Web.Components.Remotes.Services
{
    internal interface INotificationService
    {
        event Action<string>? OnMessageReceived;
        Task NotifyMessageReceived(string message);
    }
}