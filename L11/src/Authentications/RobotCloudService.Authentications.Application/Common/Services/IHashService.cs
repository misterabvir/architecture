namespace RobotCloudService.Authentications.Application.Common.Services;

public interface IHashService
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hashedPassword);
}
