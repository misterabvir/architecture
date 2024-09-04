namespace RobotCloudService.Web.Components.Remotes.Models;

public class RoomModel
{
    public string RoomId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public double Area { get; set; }
    public DateTime LastCleanedAt { get; set; } = default!;
}

