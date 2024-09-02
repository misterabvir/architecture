using MediatR;

using RobotCloudService.Application.Common;
using RobotCloudService.Authentications.Application.Common.Services;

namespace RobotCloudService.Authentications.Application.Users.Events;


public static class Registered
{
    public record Notification(Ulid UserId, string Email) : IDomainEvent;

    public class Handler(IVerificationService verificationService) : INotificationHandler<Notification>
    {
        public async Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            await verificationService.SendVerificationCodeAsync(notification.UserId, notification.Email, cancellationToken);

        }
    }
}



