using RobotCloudService.Domain.Common;

namespace RobotCloudService.Authentications.Domain.UserAggregate.ValueObjects;

public record Email(string Value) : ValueObject
{
    public const int MaxLength = 255;

    public static Email Create(string value) => new(value.ToLowerInvariant());
    public static implicit operator string(Email email) => email.Value;
}
