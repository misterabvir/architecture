using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record Area(double Value) : ValueObject
{
    public static Area Create(double value) => new(value);
    public static implicit operator double(Area square) => square.Value;
}

