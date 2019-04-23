using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Utilities
{
    public sealed class SecurePasswordHasher
    {
        static Lazy<SecurePasswordHasher> @default = new Lazy<SecurePasswordHasher>(() => new SecurePasswordHasher());
        public static SecurePasswordHasher Default => @default.Value;

        public const string DefaultScheme = "fthdgn";
        public const string DefaultVersion = "v1";
        public const int MinIteration = 1000;
        public const int MaxIteration = 10000;
        public string Scheme { get; private set; }
        public string Version { get; private set; }
        public string Prefix => $".{Scheme}.{Version}.";
        public Random Random { get; private set; }
        public SecurePasswordHasher(string scheme = DefaultScheme, string version = DefaultVersion)
        {
            Scheme = scheme ?? DefaultScheme;
            Version = version ?? DefaultVersion;
            Random = new Random();
        }
        /// <summary>
        /// Size of salt
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of hash
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Creates a hash from a password
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="iterations">number of iterations</param>
        /// <returns>the hash</returns>
        public string Hash(string password, int iterations)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            //combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            //convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            //format hash with extra information
            return string.Format("{0}{1}.{2}", Prefix, iterations, base64Hash);
        }
        /// <summary>
        /// Creates a hash from a password with random number of iterations
        /// </summary>
        /// <param name="password">the password</param>
        /// <returns>the hash</returns>
        public string Hash(string password) => Hash(password, Random.Next(MinIteration,MaxIteration));

        /// <summary>
        /// Check if hash is supported
        /// </summary>
        /// <param name="hashString">the hash</param>
        /// <returns>is supported?</returns>
        public bool IsHashSupported(string hashString) => hashString?.Contains(Prefix) ?? false;

        /// <summary>
        /// verify a password against a hash
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="hashedPassword">the hash</param>
        /// <returns>could be verified?</returns>
        public bool Verify(string password, string hashedPassword)
        {
            if (password == null || hashedPassword == null) return false;
            //check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            //extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace(Prefix, "").Split('.');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            //get hashbytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            //get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
