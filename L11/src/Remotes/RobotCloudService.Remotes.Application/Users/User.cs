using RobotCloudService.Application.Common;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Users.Entities;
using RobotCloudService.Remotes.Application.Users.Events;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users
{
    public class User : AggregateRoot
    {
        private readonly List<Room> _rooms = [];
        private readonly List<Robot> _robots = [];
        private readonly List<Log> _logs = [];
        public IReadOnlyList<Log> Logs => _logs.AsReadOnly();
        public UserId UserId { get; set; } = default!;
        public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
        public IReadOnlyList<Robot> Robots => _robots.AsReadOnly();

        private User() { } //ef
        private User(UserId userId)
        {
            UserId = userId;
        }

        public static User Create(UserId userId)
        {
            return new User(userId);
        }

        public DataOrError<Room> AddRoom(Title title, Area square)
        {
            var room = _rooms.FirstOrDefault(r => r.Title == title);
            if (room is not null)
            {
                return Error.Conflict("AddRoom.AlreadyExist", "Room already exists");
            }

            room = Room.Create(UserId, title, square);
            _rooms.Add(room);

            return room;
        }

        public DataOrError<Room> UpdateRoom(RoomId roomId, Title title, Area square)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is null)
            {
                return Error.NotFound("UpdateRoom.NotFound", "Room not found");
            }

            room.Update(title, square);

            return room;
        }

        public DataOrError<Robot> AddRobot(Model model, Speed speed)
        {
            if (_robots.Any(r => r.Model == model))
            {
                return Error.Conflict("AddRobot.AlreadyExist", "Robot already exists");
            }

            if (_robots.Count >= _rooms.Count)
            {
                return Error.Conflict("AddRobot.MaxCount", "Max robot count reached for this user");
            }

            var robot = Robot.Create(UserId, model, speed);
            _robots.Add(robot);
            return robot;
        }

        public DataOrError<Robot> UpdateRobot(RobotId robotId, Model model, Speed speed)
        {
            var robot = _robots.FirstOrDefault(r => r.RobotId == robotId);
            if (robot is null)
            {
                return Error.NotFound("UpdateRobot.NotFound", "Robot not found");
            }

            var result = robot.Update(model, speed);
            if (result.IsFailure)
            {
                return result.Error;
            }
            return robot;
        }

        public DataOrError<Robot> StartClean(RobotId robotId, RoomId roomId)
        {
            var robot = _robots.FirstOrDefault(r => r.RobotId == robotId);
            if (robot is null)
            {
                return Error.NotFound("StartClean.RobotNotFound", "Robot not found");
            }

            var room = _rooms.FirstOrDefault(r => r.RoomId == roomId);
            if (room is null)
            {
                return Error.NotFound("StartClean.RoomNotFound", "Room not found");
            }

            var result = robot.StartClean(room);
            if (result.IsSuccess)
            {
                var message = $"Robot {robot.Model.Value} started cleaning room {room.Title.Value}";
                _logs.Add(Log.Create(UserId, message));
                AddDomainEvent(new CleanStarted.Notification(UserId, message, robotId, roomId, DateTime.UtcNow, robot.CalculatedTimeOfCleaningOver));
            }

            return robot;
        }

        public DataOrError<Robot> StopClean(RobotId robotId)
        {
            var robot = _robots.FirstOrDefault(r => r.RobotId == robotId);
            if (robot is null)
            {
                return Error.NotFound("StopClean.RobotNotFound", "Robot not found");
            }

            var roomId = robot.RoomId;
            var result = robot.StopClean();
            if (result.IsFailure)
            {
                return result.Error;
            }

            var room = _rooms.First(r => r.RoomId == roomId);
            room.Cleaned();
            var message = $"Robot {robot.Model.Value} stopped cleaning room {room.Title.Value}";
            _logs.Add(Log.Create(UserId, message));
            AddDomainEvent(new CleanStoped.Notification(UserId, message, robotId, roomId, DateTime.UtcNow));

            return robot;
        }

        protected override IEnumerable<ValueObject> EqualityComponents()
        {
            yield return UserId;
        }
    }
}
