using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Utilities
{
    class PasswordGenerator
    {
        static Random random = new Random();
        const string letters = "abcdefghijklmnopqrstuvwxyz";
        const string numbers = "1234567890";
        public static char RandomCharacter(string s) => s[random.Next(s.Length)];
        public static char RandomLetter() => RandomCharacter(letters);
        public static char RandomNumber() => RandomCharacter(numbers);

        public static string Generate()
        {
            StringBuilder res = new StringBuilder();
            res.Append(RandomLetter().ToString().ToUpperInvariant())
               .Append(RandomLetter())
               .Append(RandomLetter())
               .Append(RandomLetter())
               .Append(RandomNumber())
               .Append(RandomNumber())
               .Append(RandomNumber())
               .Append(RandomNumber());
            return res.ToString();
        }
    }
}
