using System;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return React(input[0]).Length.ToString();
        }

        public string Part2(params string[] input)
        {
            var minLength = input[0].Length;

            // remove units one type at a time and fully react the resulting polymer
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                var units = React(
                    input[0]
                        .Replace(c.ToString(), string.Empty)
                        .Replace(c.ToString().ToUpper(), string.Empty)
                );

                if (units.Length < minLength)
                    minLength = units.Length;
            }

            return minLength.ToString();
        }

        private static string React(string units)
        {
            var i = 0;

            while (i + 1 < units.Length)
            {
                while (
                    // make sure index is greater than zero since it can be decremented
                    i > 0
                    // make sure index is within bounds of array since it can be incremented
                    && i + 1 < units.Length
                    // make sure the polarity (case) is different (characters are not equal)
                    && units[i] != units[i + 1]
                    // check for aA and Aa cases
                    &&
                    (units[i].ToString() == units[i + 1].ToString().ToLower() ||
                     units[i].ToString().ToLower() == units[i + 1].ToString()))
                {
                    // remove the reactive units
                    units = units.Remove(i, 2);
                    // decrement the index to examine the previous unit now that the reactive unit has been removed
                    i--;
                }

                // advance to the next unit
                i++;
            }

            return units;
        }

    }
}
