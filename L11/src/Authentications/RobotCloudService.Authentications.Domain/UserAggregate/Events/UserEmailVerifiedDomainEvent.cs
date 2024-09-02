
using RobotCloudService.Domain.Common;

namespace RobotCloudService.Authentications.Domain.UserAggregate.Events;

public record UserEmailVerifiedDomainEvent(Ulid UserId, string Email) : IDomainEvent;

