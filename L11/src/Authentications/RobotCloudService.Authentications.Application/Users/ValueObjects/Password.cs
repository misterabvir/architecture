using RobotCloudService.Application.Common;

namespace RobotCloudService.Authentications.Application.Users.ValueObjects;

public record Password(string Value) : ValueObject
{
    public const int MaxLength = 255;
    public static Password Create(string value) => new(value);
    public static implicit operator string(Password password) => password.Value;
}