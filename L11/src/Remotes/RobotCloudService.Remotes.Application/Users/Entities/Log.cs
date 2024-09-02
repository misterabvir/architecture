using RobotCloudService.Application.Common;
using RobotCloudService.Remotes.Application.Users.Snapshots;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Entities;

public class Log : Entity
{
    public LogId LogId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public DateTime OccuredAt { get; private set; }

    private Log() {} // ef

    private Log(LogId logId, UserId userId, string message)
    {
        LogId = logId;
        UserId = userId;
        Message = message;
        OccuredAt = DateTime.UtcNow;
    }

    internal static Log Create(UserId userId, string message)
    {
        return new Log(LogId.CreateUnique(), userId, message);
    }

    internal LogSnapshot ToSnapshot()
    {
        return new()
        {
            LogId = LogId,
            UserId = UserId,
            Message = Message,
            OccuredAt = OccuredAt
        };
    }

    internal static Log FromSnapshot(LogSnapshot snapshot)
    {
        return new()
        {
            LogId = snapshot.LogId,
            UserId = snapshot.UserId,
            Message = snapshot.Message,
            OccuredAt = snapshot.OccuredAt
        };
    }

    protected override IEnumerable<ValueObject> EqualityComponents()
    {
        yield return LogId;
        yield return UserId;
    }
}

