using System.Security.Cryptography;
namespace AshkanIndustrialOperations.Security;
public sealed class PasswordHasher
{
    private const int Iterations = 120_000;
    public (string Hash, string Salt) Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, 32);
        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }
    public bool Verify(string password, string expectedHash, string saltText)
    {
        try
        {
            var salt = Convert.FromBase64String(saltText);
            var expected = Convert.FromBase64String(expectedHash);
            var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, expected.Length);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
        catch (FormatException) { return false; }
    }
}
