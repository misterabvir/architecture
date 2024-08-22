using CloudService.Domain;

namespace CloudService.Application.Base.Services;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}