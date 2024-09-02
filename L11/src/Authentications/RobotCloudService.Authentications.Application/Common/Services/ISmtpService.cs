namespace RobotCloudService.Authentications.Application.Common.Services;

public interface ISmtpService
{
    Task SendVerificationCodeAsync(string email, string code, CancellationToken cancellationToken);
}
