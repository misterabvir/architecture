using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CloudService.Application.Base.Services;
using CloudService.Domain;

namespace CloudService.Infrastructure.Services;

internal class TokenGenerator(JwtSettings jwtSettings) : ITokenGenerator
{
    public string GenerateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationInMinutes);

        var claims = new[]{
                new Claim(type: ClaimTypes.NameIdentifier, value: user.UserId.ToString()),
                new Claim(type: ClaimTypes.Name, value: user.Username),
                new Claim(type: ClaimTypes.Expiration, value: expiration.ToString(CultureInfo.InvariantCulture)),
            };

        var security = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: jwtSettings.SigningCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(security);
    }
}
