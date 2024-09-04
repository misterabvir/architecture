using RobotCloudService.Remotes.Application.Users.Entities;

namespace RobotCloudService.Remotes.Presentation.Contracts;

internal static class Rooms
{
    public static class Requests
    {
        public record AddRoom(string Title, double Area)
        {
            public const string Route = "add";
            public Application.Users.Commands.AddRoom.Command ToCommand(Ulid userId)
            {
                return new Application.Users.Commands.AddRoom.Command(userId, Title, Area);
            }
        }
        public record UpdateRoom(Ulid RoomId, string Title, double Area)
        {
            public const string Route = "update";            
            public Application.Users.Commands.UpdateRoom.Command ToCommand(Ulid userId)
            {
                return new Application.Users.Commands.UpdateRoom.Command(userId, RoomId, Title, Area);
            }
        }
    }

    public static class  Responses
    {
        internal record Room
        {
            public Ulid RoomId { get; set; } = default!;
            public Ulid UserId { get; set; } = default!;
            public string Title { get; set; } = default!;
            public double Area { get; set; } = default!;
            public DateTime LastCleanedAt { get; set; } = default!;

            public static Room FromDomain(Application.Users.Entities.Room room)
            {
                return new Room
                {
                    RoomId = room.RoomId,
                    UserId = room.UserId,
                    Title = room.Title,
                    Area = room.Area,
                    LastCleanedAt = room.LastCleanedAt
                };
            }
        }
    }


    

}


