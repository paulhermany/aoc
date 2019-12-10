using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._04
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var tokens = input[0].Split('-');
            var min = int.Parse(tokens[0]);
            var max = int.Parse(tokens[1]);

            var matches = Enumerable.Range(min, max - min + 1).Where(
                n => n.ToString().GroupBy(c => c).Any(g => g.Count() > 1)
                && string.Concat(n.ToString().OrderBy(c => c)).Equals(n.ToString())
            );

            return matches.Count().ToString();
        }

        public string Part2(params string[] input)
        {
            var tokens = input[0].Split('-');
            var min = int.Parse(tokens[0]);
            var max = int.Parse(tokens[1]);

            var matches = Enumerable.Range(min, max - min + 1).Where(
                n => n.ToString().GroupBy(c => c).Any(g => g.Count() > 1)
                && string.Concat(n.ToString().OrderBy(c => c)).Equals(n.ToString())
                && n.ToString().GroupBy(c => c).Select(g => g.Count()).GroupBy(c => c).Count(g => g.Key == 2) == 1
            );

            return matches.Count().ToString();
        }

    }
}
