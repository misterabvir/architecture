
using RobotCloudService.Domain.Common;

namespace RobotCloudService.Authentications.Domain.UserAggregate.Events;

public record UserPasswordChangedDomainEvent(Ulid UserId, string Email) : IDomainEvent;

