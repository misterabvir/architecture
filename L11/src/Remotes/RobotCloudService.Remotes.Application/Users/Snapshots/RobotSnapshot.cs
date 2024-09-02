using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Snapshots;

internal class RobotSnapshot
{
    public RobotId RobotId { get; set; } = default!;
    public UserId UserId { get; set; } = default!;
    public Model Model { get; set; } = default!;
    public RoomId RoomId { get; set; } = default!;
    public Speed Speed { get; set; } = default!;
    public State RobotState { get; set; } = default!;
    public DateTime CalculatedTimeOfCleaningOver { get; set; }
}
