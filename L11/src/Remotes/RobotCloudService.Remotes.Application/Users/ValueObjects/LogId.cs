using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record LogId(Ulid Value) : ValueObject
{
    public static LogId CreateUnique() => new(Ulid.NewUlid());
    public static LogId Create(Ulid value) => new(value);
    public static implicit operator Ulid(LogId logId) => logId.Value;
}

