using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._02
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var pairCount = 0;
            var tripletCount = 0;
            foreach (var id in input)
            {
                // there has GOT to be a more elegant regex solution to this, but here's a linq quick and dirty way
                pairCount += id.GroupBy(_ => _).Any(grp => grp.Count() == 2) ? 1 : 0;
                tripletCount += id.GroupBy(_ => _).Any(grp => grp.Count() == 3) ? 1 : 0;
            }

            return (pairCount * tripletCount).ToString();
        }

        public string Part2(params string[] input)
        {
            var firstSet = input.ToArray();
            var secondSet = input.ToArray();

            // capture matching letters even if it's not the correct pair
            var sb = new StringBuilder();

            // all aboard the nested foreach train - choo choo!
            // we need to compare each string with each other string so, yeah
            foreach(var firstId in firstSet)
            {
                foreach (var secondId in secondSet)
                {
                    var diffs = 0;
                    sb.Clear();

                    // short circuit the for loop if there is more than 1 diff
                    for (var i = 0; i < firstId.Length && diffs <= 1; i++)
                    {
                        // if there's no match, increment the diffs
                        if (firstId[i] != secondId[i])
                            diffs++;
                        else
                            sb.Append(firstId[i]);
                    }

                    // boom. one diff, go ahead and return that string
                    if (diffs == 1) return sb.ToString();
                }
            }

            // fail
            return string.Empty;
        }

    }
}
