using RobotCloudService.Domain.Common;

namespace RobotCloudService.Authentications.Domain.UserAggregate.ValueObjects;

public record UserId(Ulid Value) : ValueObject
{
    public static UserId CreateUnique() => new(Ulid.NewUlid());
    public static UserId Create(Ulid value) => new(value);
    public static implicit operator Ulid(UserId userId) => userId.Value;
}
