namespace RobotCloudService.Web.Components.Remotes.Models;

public class RobotModel
{
    public string RobotId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double Speed { get; set; }
    public string RobotState { get; set; } = string.Empty;
    public DateTime CalculatedTimeOfCleaningOver { get; set; }
}

