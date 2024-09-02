namespace RobotCloudService.Notifications;

public record CleanStoppedEvent(Ulid UserId, string Message, Ulid RobotId, Ulid RoomId, DateTime OccurredOn);
