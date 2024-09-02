using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record Speed(double Value) : ValueObject
{
    public static Speed Create(double value) => new(value);
    public static implicit operator double(Speed speed) => speed.Value;
}

