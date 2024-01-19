
using System.Security.Cryptography;
using System.Text;

namespace PitchMatch.Securituy
{
    public static class PasswordHasher
    {

        public static string HashPassword(string unhashedPassword)
        {
            // Create an instance of SHA-256
            using (var sha256 = SHA256.Create())
            {
                // Convert the password to a byte array
                byte[] passwordBytes = Encoding.UTF8.GetBytes(unhashedPassword);

                // Compute the hash
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hashed bytes to a base64-encoded string for storage
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword;
            }
        }
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            string salt = Convert.ToBase64String(saltBytes);
            return salt;
        }
    }
}
