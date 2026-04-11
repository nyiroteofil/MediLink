using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MediLink_BackEnd.Data
{
    public class PwdEncryptionHelper
    {
        // takes the password string as input and outputs hashed password using pbkdf2 encryption
        public static string GetHashedPassword(string password, out string salt)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            salt = Convert.ToBase64String(saltBytes);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            return hash;

        }

        public static bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            ));

            return computedHash == hash;
        }
    }
}
