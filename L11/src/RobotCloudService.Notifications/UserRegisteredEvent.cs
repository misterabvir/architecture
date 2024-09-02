namespace RobotCloudService.Notifications;

public record UserRegisteredEvent(Ulid UserId, DateTime OccuredOn);

