using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Snapshots;

internal class LogSnapshot
{
    public LogId LogId { get; set; } = default!;
    public UserId UserId { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime OccuredAt { get; set; }
}
