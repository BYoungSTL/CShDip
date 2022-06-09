using System.Security.Cryptography;
using System.Text;
using ComputerShopDB.Common.Cryptography;
using ComputerShopDB.Helpers;

namespace ComputerShopDB.Common.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt(int length)
        {
            Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            using var cryptoService = new RNGCryptoServiceProvider();
            var saltBytes = new byte[length];
            cryptoService.GetNonZeroBytes(saltBytes);
            return Encoding.Unicode.GetString(saltBytes);
        }

        public static string ComputeHash(string password, string salt)
        {
            Check.ArgumentNotNull(password, nameof(password));
            Check.ArgumentNotNull(salt, nameof(salt));

            var hashAlgorithm = new Sha512Hash();
            return hashAlgorithm.CalculateHash(password + salt);
        }

        public static string Generate(int length)
        {
            Check.ArgumentSatisfies(length, x => x > 0, "Value must be > 0.", nameof(length));

            var randomString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Trim();
            return length <= randomString.Length ? randomString[..length] : randomString;
        }
    }
}
