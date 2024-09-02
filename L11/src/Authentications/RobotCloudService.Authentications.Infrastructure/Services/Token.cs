using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Authentications.Application.Users;

namespace RobotCloudService.Authentications.Infrastructure.Services;

public static class Token
{
    public static IServiceCollection AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetSection(Settings.SectionName).Get<Settings>()
            ?? throw new NullReferenceException($"Configuration section '{Settings.SectionName}' not found.");
        services.AddSingleton(tokenSettings);
        services.AddTransient<ITokenService, Service>();
        return services;
    }
    
    internal class Settings
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

    internal class Service(Settings settings) : ITokenService
    {
        public string GetToken(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(settings.ExpirationInMinutes);

            var claims = new[]{
                new Claim(type: ClaimTypes.NameIdentifier, value: user.UserId.Value.ToString()),
                new Claim(type: ClaimTypes.Email, value: user.Email.Value),
                new Claim(type: ClaimTypes.Name, value: user.Email.Value),
                new Claim(type: ClaimTypes.Expiration, value: expiration.ToString(CultureInfo.InvariantCulture)),
            };

            var security = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: settings.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(security);
        }
    }
}


