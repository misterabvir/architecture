using RobotCloudService.Application.Common;

namespace RobotCloudService.Remotes.Application.Users.ValueObjects;

public record RoomId(Ulid Value) : ValueObject
{
    public static RoomId CreateUnique() => new(Ulid.NewUlid());
    public static RoomId Create(Ulid value) => new(value);
    public static RoomId Empty => new(Ulid.Empty);
    public static implicit operator Ulid(RoomId roomId) => roomId.Value;
}

