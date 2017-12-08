using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._06
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input) =>
            string.Concat(
                input.Pivot()
                .Select(line => 
                    line
                        .GroupBy(chr => chr)
                        .OrderByDescending(_ => _.Count())
                        .First()
                        .Key
                )
            );

        public string Part2(params string[] input) =>
            string.Concat(
                input.Pivot()
                    .Select(line =>
                        line
                            .GroupBy(chr => chr)
                            .OrderBy(_ => _.Count())
                            .First()
                            .Key
                    )
            );
    }
}
