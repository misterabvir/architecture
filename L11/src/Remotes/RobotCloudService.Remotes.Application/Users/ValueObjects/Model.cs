using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record Model(string Value) : ValueObject
{
    public static Model Create(string value) => new(value);
    public static implicit operator string(Model title) => title.Value;
}

