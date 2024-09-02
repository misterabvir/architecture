using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record State(string Value) : ValueObject
{
    public static State Create(string value) => new(value);
    public static implicit operator string(State robotState) => robotState.Value;

    public static State Idle => new("Idle");
    public static State Cleaning => new("Cleaning");
}

