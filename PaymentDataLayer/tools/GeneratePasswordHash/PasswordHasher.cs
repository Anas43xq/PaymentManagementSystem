using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    public const int Iterations = 100000;

    public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        const int saltLen = 16;
        const int hashLen = 32;

        using (var rng = RandomNumberGenerator.Create())
        {
            salt = new byte[saltLen];
            rng.GetBytes(salt);
        }

        using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            hash = derive.GetBytes(hashLen);
        }
    }

    public static string ToHex(byte[] data)
    {
        return BitConverter.ToString(data).Replace("-", "");
    }

    public static string GetInsertSnippet(string username, string email, byte[] hash, byte[] salt)
    {
        var hashHex = ToHex(hash);
        var saltHex = ToHex(salt);
        return $"INSERT INTO dbo.Users (Username, Email, PasswordHash, PasswordSalt, RoleId) VALUES ('{username}','{email}', 0x{hashHex}, 0x{saltHex}, (SELECT RoleId FROM dbo.Roles WHERE Name='Admin'));";
    }
}
