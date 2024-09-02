namespace RobotCloudService.Web.Components.Remotes.Models
{
    public class UserData
    {
        public string UserId { get; set; } = string.Empty;
        public List<Room> Rooms { get; set; } = [];
        public List<Robot> Robots { get; set; } = [];
    }

    public class Log
    {
        public string LogId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime OccuredAt { get; set; }
    }


    public class AddRoom
    {
        public string Title { get; set; } = string.Empty;
        public double Area { get; set; }
    }

    public class UpdatedRoom
    {
        public string RoomId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public double Area { get; set; }
    }

    public class AddRobot
    {
        public string Model { get; set; } = string.Empty;
        public double Speed { get; set; }
    }

    public class UpdatedRobot
    {
        public string RobotId { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double Speed { get; set; }
    }

    public class StartCleanModel
    {
        public string RoomId { get; set; } = string.Empty;
        public string RobotId { get; set; } = string.Empty;
    }

    public class Room
    {
        public string RoomId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public double Area { get; set; }
        public DateTime LastCleanedAt { get; set; } = default!;
    }

    public class Robot
    {
        public string RobotId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public double Speed { get; set; }
        public string RobotState { get; set; } = string.Empty;
        public DateTime CalculatedTimeOfCleaningOver { get; set; }
    }


}

