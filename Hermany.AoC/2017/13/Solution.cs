using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._13
{
    public class Solution : ISolution
    {
        public static int ReflectiveMod(int v, int m) =>
            Math.Abs((m - 1) - (v + (m - 1)) % ((m - 1) * 2));

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
