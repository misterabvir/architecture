
using RobotCloudService.Application.Common;

namespace RobotCloudService.Authentications.Application.Users.Events;

public record UserPasswordChangedDomainEvent(Ulid UserId, string Email) : IDomainEvent;

