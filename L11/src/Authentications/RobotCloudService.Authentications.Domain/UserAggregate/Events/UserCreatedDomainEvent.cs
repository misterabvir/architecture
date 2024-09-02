using RobotCloudService.Domain.Common;

namespace RobotCloudService.Authentications.Domain.UserAggregate.Events;

public record UserCreatedDomainEvent(Ulid UserId, string Email) : IDomainEvent;

