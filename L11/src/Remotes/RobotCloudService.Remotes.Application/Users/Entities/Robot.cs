using RobotCloudService.Application.Common;
using RobotCloudService.Application.Results;
using RobotCloudService.Remotes.Application.Users.ValueObjects;

namespace RobotCloudService.Remotes.Application.Users.Entities;

public class Robot : Entity
{
    public RobotId RobotId { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public Model Model { get; private set; } = default!;
    public Speed Speed { get; private set; } = default!;
    public State RobotState { get; private set; } = default!;
    public RoomId RoomId { get; private set; } = default!;
    public DateTime CalculatedTimeOfCleaningOver { get;private set; }

    private Robot() { } //ef    
    private Robot(RobotId robotId, UserId userId, Model model, Speed speed)
    {
        RobotId = robotId;
        UserId = userId;
        Model = model;
        RoomId = RoomId.Empty;
        RobotState = State.Idle;
        Speed = speed;
    }
    
    public static Robot Create( UserId userId, Model model, Speed speed)
    {
        return new Robot(RobotId.CreateUnique(), userId, model, speed);
    }


    public SuccessOrError StartClean(Room room)
    {
        if (RobotState != State.Idle)
            return Error.Conflict("Start.Cleaning", "Robot is not idle");

        CalculatedTimeOfCleaningOver = DateTime.UtcNow.AddSeconds(room.Area.Value / Speed.Value);
        RobotState = State.Cleaning;
        RoomId = room.RoomId;

        return SuccessOrError.Success;
    }

    public SuccessOrError StopClean()
    {       
        if (RobotState != State.Cleaning)
            return Error.Conflict("StopCleaning.Idle", "Robot is not cleaning");

        RobotState = State.Idle;
        RoomId = RoomId.Empty;
        return SuccessOrError.Success;
    }

    internal SuccessOrError Update(Model model, Speed speed)
    {
        if(RobotState != State.Idle)
        {
            return Error.Conflict("Update.Robot", "Robot is not idle");
        }
        
        Model = model;
        Speed = speed;
        return SuccessOrError.Success;
    }

    protected override IEnumerable<ValueObject> EqualityComponents()
    {
        yield return RobotId;
        yield return UserId;
    }
}
