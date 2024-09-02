using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotCloudService.Authentications.Application.Common.Services;
using RobotCloudService.Application.Results;

namespace RobotCloudService.Authentications.Infrastructure.Services;


public static class Verification
{
    internal static IServiceCollection AddVerification(this IServiceCollection services, IConfiguration configuration)
    {

        var verificationSettings = configuration.GetSection(Settings.SectionName).Get<Settings>()
            ?? throw new NullReferenceException($"Configuration section '{Settings.SectionName}' not found.");
        services.AddSingleton(verificationSettings);
        services.AddTransient<IVerificationService, Service>();
        return services;
    }
   
    
    internal class Settings
    {
        public const string SectionName = "Settings:Verification";
        public int VerificationCodeLength { get; init; }
        public int TimeOutExpirationInSeconds { get; init; }
        public int CodeExpirationMinutes { get; init; }

        public DistributedCacheEntryOptions TimeOutOptions => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(TimeOutExpirationInSeconds)
        };

        public DistributedCacheEntryOptions CodeExpirationOptions => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CodeExpirationMinutes)
        };

        public string GetRandomCode() => Guid.NewGuid().ToString()[..VerificationCodeLength];
        public static string GetVerificationCodeKey(Ulid userId) => $"VerificationCode:{userId}";
        public static string GetTimeOutKey(Ulid userId) => $"TimeOut:{userId}";
    }



    internal class Service(
        Settings settings,
        IDistributedCache cache,
        ISmtpService smtpService) : IVerificationService
    {
        public async Task<SuccessOrError> SendVerificationCodeAsync(Ulid userId, string email, CancellationToken cancellationToken)
        {
            var cachedTimeOut = await cache.GetStringAsync(Settings.GetTimeOutKey(userId), token: cancellationToken);
            if (cachedTimeOut is not null)
            {
                return Error.Forbidden("Verification.TimeOut", "You have to wait for a while before generating a new verification code.");
            }

            var code = settings.GetRandomCode();
            await cache.SetStringAsync(Settings.GetVerificationCodeKey(userId), code, settings.CodeExpirationOptions, cancellationToken);
            await cache.SetStringAsync(Settings.GetTimeOutKey(userId), "true", settings.TimeOutOptions, cancellationToken);

            await smtpService.SendVerificationCodeAsync(email, code, cancellationToken);

            return SuccessOrError.Success;
        }

        public async Task<SuccessOrError> VerifyCodeAsync(Ulid userId, string code, CancellationToken cancellationToken)
        {
            var cachedCode = await cache.GetStringAsync(Settings.GetVerificationCodeKey(userId), token: cancellationToken);
            if (cachedCode is null)
            {
                return Error.Forbidden("Verification.NotVerified", "You have to generate a verification code first.");
            }
            if (cachedCode != code)
            {
                return Error.Forbidden("Verification.IncorrectCode", "The verification code is incorrect.");
            }
            await cache.RemoveAsync(Settings.GetVerificationCodeKey(userId), cancellationToken);
            await cache.RemoveAsync(Settings.GetTimeOutKey(userId), cancellationToken);
            return SuccessOrError.Success;
        }
    }
}


