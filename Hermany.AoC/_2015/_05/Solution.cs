using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hermany.AoC.Common;

namespace Hermany.AoC._2015._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return input.Count(IsNice).ToString();
        }

        public string Part2(params string[] input)
        {
            return input.Count(IsNice2).ToString();
        }

        public bool IsNice(string value)
        {
            var vowels = "aeiou".ToCharArray();
            var doubles = "abcdefghijklmnopqrstuvwxyz".Select(_ => string.Concat(_, _)).ToArray();
            var excludes = new[] {"ab", "cd", "pq", "xy"};
            return
                value.Count(_ => vowels.Contains(_)) >= 3
                && doubles.Any(value.Contains)
                && !excludes.Any(value.Contains);
        }

        public bool IsNice2(string value)
        {
            return
                ContainsRepeatingPairWithoutOverlap(value)
                && ContainsRepeatingWithOneLetterBetween(value);
        }

        public bool ContainsRepeatingPairWithoutOverlap(string value)
        {
            var i = 0;
            while (i < value.Length - 1)
            {
                var a = value[i];
                var b = value[i + 1];

                if (value.IndexOf(string.Concat(a, b), i+2, StringComparison.Ordinal) > -1) return true;

                i++;
            }

            return false;
        }

        public bool ContainsRepeatingWithOneLetterBetween(string value)
        {
            var i = 0;
            while (i < value.Length - 2)
            {
                if (value[i] == value[i + 2]) return true;
                i++;
            }

            return false;
        }
    }
}
