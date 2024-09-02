using RobotCloudService.Authentications.Application.Users;

namespace RobotCloudService.Authentications.Application.Common.Services;

public interface ITokenService
{
    string GetToken(User user);
}
