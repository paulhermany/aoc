using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._13
{
    public class Solution : ISolution
    {
        public static int ReflectiveMod(int value, int modulus)
        {
            // ReflectiveMod(6, 4) = 0
            // value: 0 1 2 3 4 5 6 7 8 9 10
            // r-mod: 0 1 2 3 2 1 0 1 2 3 4 

            var p = (modulus - 1) * 2;
            var v = value % p;
            return v >= p / 2 ? p - v : v;
            
        }
        public string Part1(params string[] input)
        {
            var ranges = input.Select(line =>
            {
                var tokens = line.Split(':').Select(_ => _.Trim()).ToArray();
                return new { Index = int.Parse(tokens[0]), Range = int.Parse(tokens[1]) };
            }).ToArray();

            return ranges.Where(_ => ReflectiveMod(_.Index, _.Range) == 0).Sum(_ => _.Index * _.Range).ToString();
        }
        
        public string Part2(params string[] input)
        {
            var ranges = input.Select(line =>
            {
                var tokens = line.Split(':').Select(_ => _.Trim()).ToArray();
                return new { Index = int.Parse(tokens[0]), Range = int.Parse(tokens[1]) };
            }).ToArray();

            var i = 0;

            while (ranges.Any(_ => ReflectiveMod(_.Index + i, _.Range) == 0)) i++;

            return i.ToString();
        }
    }
}
