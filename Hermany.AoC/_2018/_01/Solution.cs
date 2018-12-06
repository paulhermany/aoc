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
            return Array.ConvertAll(input, int.Parse).Sum().ToString();
        }

        public string Part2(params string[] input)
        {
            var frequencyChanges = Array.ConvertAll(input, int.Parse);

            var currentFrequency = 0;
            var frequencies = new HashSet<int>();
            var index = 0;

            while (!frequencies.Contains(currentFrequency))
            {
                frequencies.Add(currentFrequency);
                currentFrequency += frequencyChanges[index];
                index = (index + 1) % frequencyChanges.Length;
            }

            return currentFrequency.ToString();
        }
    }
}
