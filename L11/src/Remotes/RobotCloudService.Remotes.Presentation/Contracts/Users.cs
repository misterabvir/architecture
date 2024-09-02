using RobotCloudService.Remotes.Application.Users;

namespace RobotCloudService.Remotes.Presentation.Contracts;

internal static class Users
{
    public static class Routes
    {
        public const string UserData = "users/data";
        public const string UserLogs = "users/logs";
    }
        
    public static class Responses
    {
        internal record Log
        {
            public Ulid LogId { get; set; } = default!;
            public Ulid UserId { get; set; } = default!;
            public string Message { get; set; } = default!;
            public DateTime OccuredAt { get; set; }
            public static Log FromDomain(Application.Users.Entities.Log log)
            {
                return new Log
                {
                    LogId = log.LogId,
                    UserId = log.UserId,
                    Message = log.Message,
                    OccuredAt = log.OccuredAt
                };
            }
        }

        internal record UserData
        {
           
            public Ulid UserId { get; set; } = default!;
            public IEnumerable<Rooms.Responses.Room> Rooms { get; set; } = [];
            public IEnumerable<Robots.Responses.Robot> Robots { get; set; } = [];

            public static UserData FromDomain(User user)
            {
                return new UserData
                {
                    UserId = user.UserId,
                    Rooms = user.Rooms.Select(Contracts.Rooms.Responses.Room.FromDomain),
                    Robots = user.Robots.Select(Contracts.Robots.Responses.Robot.FromDomain)
                };
            }
        }
    }

    
}


