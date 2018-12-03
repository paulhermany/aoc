using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return input.Select(_ => (_[0] == '-' ? -1 : 1) * Convert.ToInt64(_.Substring(1))).Sum()
                .ToString();
        }

        public string Part2(params string[] input)
        {
            var changes = input.Select(_ => (_[0] == '-' ? -1 : 1) * Convert.ToInt64(_.Substring(1))).ToArray();

            long freq = 0;
            var freqs = new HashSet<long>();
            var index = 0;

            while (!freqs.Contains(freq))
            {
                freqs.Add(freq);
                freq += changes[index];
                index = (index + 1) % changes.Length;
            }

            return freq.ToString();
        }
    }
}
