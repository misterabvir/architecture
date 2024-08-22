using System.Security.Cryptography;
using CloudService.Application.Base.Services;

namespace CloudService.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
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

    public bool Verify(string password, string passwordHash)
    {
        byte[] salt = Convert.FromHexString(passwordHash.Split('.').First());
        byte[] hash =  Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName,
            outputLength: HashSize);

        var hashToVerify = $"{Convert.ToHexString(salt)}.{Convert.ToHexString(hash)}";

        return passwordHash.Equals(hashToVerify, StringComparison.OrdinalIgnoreCase);
    }
}