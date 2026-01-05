using System;
using System.Security.Cryptography;

namespace PaymentDataLayer
{
    public static class AuthHelper
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

        public static bool VerifyPassword(string password, byte[] expectedHash, byte[] salt)
        {
            using (var derive = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                var computed = derive.GetBytes(expectedHash.Length);

                return FixedTimeEquals(computed, expectedHash);
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            int result = 0;
            if (a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
    }
}
