using RobotCloudService.Application.Common;
using RobotCloudService.Remotes.Application.Users.Snapshots;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Entities;

public class Room : Entity
{
    public RoomId RoomId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public Title Title { get; private set; } = default!;
    public Area Area { get; private set; } = default!;
    public DateTime LastCleanedAt { get; private set; } = default!;

    private Room() { }  //ef
    private Room(RoomId roomId, UserId userId, Title title, Area square)
    {
        RoomId = roomId;
        Title = title;
        Area = square;
        UserId = userId;
        LastCleanedAt = DateTime.MinValue.ToUniversalTime();
    }
    
    public static Room Create(UserId userId, Title title, Area square)
    {
        return new Room(RoomId.CreateUnique(), userId, title, square);
    }

    internal void Update(Title title, Area square)
    {
        Title = title;
        Area = square; 
    }
    internal void Cleaned()
    {
        LastCleanedAt = DateTime.UtcNow;
    }

    internal RoomSnapshot ToSnapshot()
    {
        return new RoomSnapshot()
        {
            RoomId = RoomId,
            UserId = UserId,
            Title = Title,
            Area = Area,
            LastCleanedAt = LastCleanedAt
        };
    }

    internal static Room FromSnapshot(RoomSnapshot snapshot)
    {
        return new Room()
        {
            RoomId = snapshot.RoomId,
            UserId = snapshot.UserId,
            Title = snapshot.Title,
            Area = snapshot.Area,
            LastCleanedAt = snapshot.LastCleanedAt
        };
    }

    protected override IEnumerable<ValueObject> EqualityComponents()
    {
        yield return RoomId;
        yield return UserId;
    }
}
