using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._01
{
    public class Solution : ISolution
    {
        public string[] Part1(params string[] input)
        {
            // TODO: ISolution.Part1/Part2 doesn't need to return string array. String is fine.
            return new[]
            {
                // the meat of the problem: summing negative and positive frequency changes
                input.Select(_ => (_[0] == '-' ? -1 : 1) * Convert.ToInt64(_.Substring(1))).Sum()
                    .ToString()
            };
        }

        public string[] Part2(params string[] input)
        {
            // same as part 1 only not summing them - just get list of chnages
            var changes = input.Select(_ => (_[0] == '-' ? -1 : 1) * Convert.ToInt64(_.Substring(1)));

            long freq = 0;
            var freqs = new HashSet<long>();

            // hot damn, not proud of this while(true) thing... need to loop through the changes until you reach a repeated freq
            while (true)
            {
                foreach (var change in changes)
                {
                    if (freqs.Contains(freq)) return new[] {freq.ToString()};
                    freqs.Add(freq);
                    freq += change;
                }
            }
        }
    }
}
