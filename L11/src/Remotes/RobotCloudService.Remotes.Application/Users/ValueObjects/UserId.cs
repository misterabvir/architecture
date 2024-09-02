using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record UserId(Ulid Value) : ValueObject
{
    public static UserId CreateUnique() => new(Ulid.NewUlid());
    public static UserId Create(Ulid value) => new(value);
    public static implicit operator Ulid(UserId userId) => userId.Value;
    public static implicit operator string(UserId userId) => userId.Value.ToString();
}