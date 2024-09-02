using System.Security.Cryptography;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RobotCloudService.Authentications.Application.Common.Services;

namespace RobotCloudService.Authentications.Infrastructure.Services;

public static class Hash
{
    internal static IServiceCollection AddHashService(this IServiceCollection services, IConfiguration configuration)
    {
        var hashSettings = configuration.GetSection(Settings.SectionName).Get<Settings>()
            ?? throw new NullReferenceException($"Configuration section '{Settings.SectionName}' not found.");
        services.AddSingleton(hashSettings);
        services.AddTransient<IHashService, Service>();
        return services;
    }

    internal class Settings
    {
        public const string SectionName = "Settings:Hash";

        public int SaltSize { get; set; }
        public int HashSize { get; set; }
        public int Iterations { get; set; }
        public HashAlgorithmName HashAlgorithmName { get; set; }
    }

    internal class Service : IHashService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName,
                outputLength: HashSize);

            return $"{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            byte[] salt = Convert.FromHexString(passwordHash.Split('.').First());
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName,
                outputLength: HashSize);

            var hashToVerify = $"{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";

            return passwordHash.Equals(hashToVerify, StringComparison.OrdinalIgnoreCase);
        }
    }
}
