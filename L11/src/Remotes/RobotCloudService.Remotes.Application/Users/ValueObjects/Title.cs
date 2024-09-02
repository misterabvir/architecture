using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record Title(string Value) : ValueObject
{
    public static Title Create(string value) => new(value);
    public static implicit operator string(Title title) => title.Value;
}

