using System;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var units = input[0];
            var i = 0;

            while (i + 1 < units.Length)
            {
                while (i > 0 && i+1 < units.Length && units[i] != units[i + 1] &&
                       (units[i].ToString() == units[i + 1].ToString().ToLower() ||
                        units[i].ToString().ToLower() == units[i + 1].ToString()))
                {
                    units = units.Remove(i, 2);
                    i--;
                }

                i++;
            }

            return units.Length.ToString();
        }

        public string Part2(params string[] input)
        {
            var units = string.Empty;
            var i = 0;

            var minLength = input[0].Length;

            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                units = input[0].Replace(c.ToString(), string.Empty).Replace(c.ToString().ToUpper(), string.Empty);
                i = 0;

                while (i + 1 < units.Length)
                {
                    while (i > 0 && i + 1 < units.Length && units[i] != units[i + 1] &&
                           (units[i].ToString() == units[i + 1].ToString().ToLower() ||
                            units[i].ToString().ToLower() == units[i + 1].ToString()))
                    {
                        units = units.Remove(i, 2);
                        i--;
                    }

                    i++;
                }

                if (units.Length < minLength) minLength = units.Length;
            }

            return minLength.ToString();
        }
        
    }
}
