using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hermany.AoC._2016._05
{
    public class Solution : ISolution
    {
        public int PasswordLength { get; set; } = 8;
        public string Part1(params string[] input)
        {
            var sb = new StringBuilder();

            var index = 0;

            while (sb.Length < PasswordLength)
            {
                var hash = CalculateMd5Hash(string.Concat(input[0], index));
                if (hash.StartsWith("00000"))
                    sb.Append(hash[5]);
                index++;
            }

            return sb.ToString();
        }

        public string Part2(params string[] input)
        {
            var password = string.Empty.PadLeft(PasswordLength).ToCharArray();

            var found = 0;
            var index = 0;

            while (found < password.Length)
            {
                var hash = CalculateMd5Hash(string.Concat(input[0], index));
                if (hash.StartsWith("00000"))
                {
                    var value = (int)char.GetNumericValue(hash[5]);
                    if (value < password.Length && value >= 0 && password[value] == ' ')
                    {
                        found++;
                        password[value] = hash[6];
                    }
                }
                index++;
            }

            return string.Concat(password);
        }

        public static string CalculateMd5Hash(string input)
        {
            var hash = Md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            
            return string.Concat(hash.Select(_ => _.ToString("x2")));
        }

        private static readonly MD5 Md5 = MD5.Create();
    }
}
