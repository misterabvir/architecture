namespace RobotCloudService.Remotes.Presentation.Contracts;

internal static class Robots
{
    internal static class Requests
    {
        public record AddRobot(string Model, double Speed)
        {
            public const string Route = "robots/add";
            public Application.Users.Commands.AddRobot.Command ToCommand(Ulid userId)
            {
                return new Application.Users.Commands.AddRobot.Command(userId, Model, Speed);
            }
        }

        public record UpdateRobot(Ulid RobotId, string Model, double Speed)
        {
            public const string Route = "robots/update";
            public Application.Users.Commands.UpdateRobot.Command ToCommand(Ulid userId)
            {
                return new Application.Users.Commands.UpdateRobot.Command(userId, RobotId, Model, Speed);
            }
        }

        public record StartClean(Ulid RobotId, Ulid RoomId)
        {
            public const string Route = "robots/start";
            public Application.Users.Commands.StartClean.Command ToCommand(Ulid userId)
            {
                return new Application.Users.Commands.StartClean.Command(userId, RoomId, RobotId);
            }
        }
    }

    public static class Responses
    {

        internal record Robot
        {
            public Ulid RobotId { get; set; } = default!;
            public Ulid UserId { get; set; } = default!;
            public Ulid RoomId { get; set; } = default!;
            public string Model { get; set; } = default!;
            public double Speed { get; set; } = default!;
            public string RobotState { get; set; } = default!;
            public DateTime CalculatedTimeOfCleaningOver { get; set; }


            public static Robot FromDomain(Application.Users.Entities.Robot robot)
            {
                return new Robot
                {
                    RobotId = robot.RobotId,
                    UserId = robot.UserId,
                    RoomId = robot.RoomId,
                    Model = robot.Model,
                    Speed = robot.Speed,
                    RobotState = robot.RobotState.Value,
                    CalculatedTimeOfCleaningOver = robot.CalculatedTimeOfCleaningOver
                };
            }
        }

    }

}


