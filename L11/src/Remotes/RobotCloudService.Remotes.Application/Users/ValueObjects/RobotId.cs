using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record RobotId(Ulid Value) : ValueObject
{
    public static RobotId CreateUnique() => new(Ulid.NewUlid());
    public static RobotId Create(Ulid value) => new(value);
    public static implicit operator Ulid(RobotId userId) => userId.Value;
}

