namespace RobotCloudService.Web.Components.Remotes.Models;

public class UserDataModel
{
    public string UserId { get; set; } = string.Empty;
    public List<RoomModel> Rooms { get; set; } = [];
    public List<RobotModel> Robots { get; set; } = [];
}

