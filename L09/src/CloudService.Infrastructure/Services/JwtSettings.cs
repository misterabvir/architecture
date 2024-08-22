using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CloudService.Infrastructure.Services;

internal class JwtSettings
{
    public const string SectionName = "Settings:Token";
    public required string Secret { get; init; }
    public required string Issuer { get; init; }
    public required string Audience { get; init; }

    public required int ExpirationInMinutes { get; init; }
    public required bool ValidateIssuer { get; init; }
    public required bool ValidateAudience { get; init; }
    public required bool ValidateLifeTime { get; init; }
    public required bool ValidateIssuerSigningKey { get; init; }

    public SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(Secret));
    public SigningCredentials SigningCredentials => new(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

    public TokenValidationParameters TokenValidationParameters => new()
    {
        ValidateIssuer = ValidateIssuer,
        ValidateAudience = ValidateAudience,
        ValidateLifetime = ValidateLifeTime,
        ValidateIssuerSigningKey = ValidateIssuerSigningKey,
        ValidIssuer = Issuer,
        ValidAudience = Audience,
        IssuerSigningKey = SymmetricSecurityKey
    };
}