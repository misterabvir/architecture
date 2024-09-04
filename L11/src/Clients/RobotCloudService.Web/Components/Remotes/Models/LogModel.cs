namespace RobotCloudService.Web.Components.Remotes.Models;

public class LogModel
{
    public string LogId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime OccuredAt { get; set; }
}

