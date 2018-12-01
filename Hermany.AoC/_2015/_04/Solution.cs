using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;

namespace Hermany.AoC._2015._04
{
    public class Solution : ISolution
    {
        public string[] Part1(params string[] input)
        {
            var md5 = MD5.Create();
            return input.Select(_ => GetIndexOfMd5HashStartingWith(md5, _).ToString()).ToArray();            
        }

        public string[] Part2(params string[] input)
        {
            var md5 = MD5.Create();
            return input.Select(_ => GetIndexOfMd5HashStartingWith(md5, _, "000000").ToString()).ToArray();
        }

        public int GetIndexOfMd5HashStartingWith(MD5 md5, string secretKey, string startsWith = "00000")
        {
            var i = 1;
            while (true)
            {
                var bytes = Encoding.ASCII.GetBytes(string.Concat(secretKey, i));
                var hash = md5.ComputeHash(bytes);
                var guid = ByteArrayToString(hash);
                if (guid.StartsWith(startsWith)) return i;
                i++;
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
