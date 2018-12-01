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
            // same as part 1 only not summing them - just get list of changes
            var changes = input.Select(_ => (_[0] == '-' ? -1 : 1) * Convert.ToInt64(_.Substring(1)));

            long freq = 0;
            var freqs = new HashSet<long>();

            // hot damn, not proud of this while(true) thing... need to loop through the changes until you reach a repeated freq
            while (true)
            {
                foreach (var change in changes)
                {
                    if (freqs.Contains(freq)) return freq.ToString();
                    freqs.Add(freq);
                    freq += change;
                }
            }
        }
    }
}
