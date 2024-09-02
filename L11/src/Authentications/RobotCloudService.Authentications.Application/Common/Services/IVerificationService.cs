using RobotCloudService.Authentications.Application.Users.ValueObjects;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Application.Common.Services;

public interface IVerificationService
{
    Task<SuccessOrError> SendVerificationCodeAsync(Ulid userId, string email, CancellationToken cancellationToken = default);
    Task<SuccessOrError> VerifyCodeAsync(Ulid userId, string code, CancellationToken cancellationToken = default);
}
