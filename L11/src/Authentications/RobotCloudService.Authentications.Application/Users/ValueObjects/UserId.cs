using RobotCloudService.Application.Common;

namespace RobotCloudService.Authentications.Application.Users.ValueObjects;

public record UserId(Ulid Value) : ValueObject
{
    public static UserId CreateUnique() => new(Ulid.NewUlid());
    public static UserId Create(Ulid value) => new(value);
    public static implicit operator Ulid(UserId userId) => userId.Value;
}
