using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Snapshots;

internal class RoomSnapshot
{
    public RoomId RoomId { get; set; } = default!;
    public UserId UserId { get; set; } = default!;
    public Title Title { get; set; } = default!;
    public Area Area { get; set; } = default!;
    public DateTime LastCleanedAt { get; set; } = default!;

}