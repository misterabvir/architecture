namespace RobotCloudService.Notifications;

public record CleanStartedEvent(Ulid UserId, string Message, Ulid RobotId, Ulid RoomId, DateTime OccurredOn);
