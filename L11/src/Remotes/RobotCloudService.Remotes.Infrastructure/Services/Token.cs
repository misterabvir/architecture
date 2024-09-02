using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace RobotCloudService.Remotes.Infrastructure.Authorizations;

public static class Token
{
    public static IServiceCollection AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetSection(Settings.SectionName).Get<Settings>()
            ?? throw new NullReferenceException($"Configuration section '{Settings.SectionName}' not found.");
        services.AddSingleton(tokenSettings);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = tokenSettings.TokenValidationParameters);
        services.AddAuthorization();
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
}
