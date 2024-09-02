using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Snapshots;

internal class UserSnapshot
{
    public UserId UserId { get; set; } = default!;
    public List<LogSnapshot> Logs { get; set; } = [];
    public List<RoomSnapshot> Rooms { get; set; } = [];
    public List<RobotSnapshot> Robots { get; set; } = [];
}
