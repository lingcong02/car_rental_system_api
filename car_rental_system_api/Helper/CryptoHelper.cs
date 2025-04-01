using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace car_rental_system_api.Helper
{
    public class CryptoHelper
    {
        public static (byte[] hash, Guid guid) HashPassword(string password)
        {
            Guid guid = Guid.NewGuid(); // Generate a unique salt
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] encryptedPassword = Encoding.UTF8.GetBytes(password + guid);
                byte[] hash = sha256.ComputeHash(encryptedPassword);
                return (hash, guid);
            }
        }

        public static bool VerifyPassword(string password, byte[] storedHash, Guid storedGuid)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] encryptedPassword = Encoding.UTF8.GetBytes(password + storedGuid);
                byte[] hash = sha256.ComputeHash(encryptedPassword);
                return StructuralComparisons.StructuralEqualityComparer.Equals(hash, storedHash);
            }
        }
    }
}
