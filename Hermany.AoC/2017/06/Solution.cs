using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2017._06
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var slots = input[0].Split('\t').Select(int.Parse).ToArray();

            var reallocationCount = 0;
            var allocations = new HashSet<string>();

            while (!allocations.Contains(string.Join(",", slots)))
            {
                allocations.Add(string.Join(",", slots));

                var maxIndex = slots.Select((value, index) => new { Value = value, Index = index })
                    .GroupBy(_ => _.Value)
                    .OrderByDescending(grp => grp.Key)
                    .First().First().Index;

                var memory = slots[maxIndex];
                slots[maxIndex] = 0;

                for (var i = 0; i < memory; i++)
                    slots[(maxIndex + 1 + i) % slots.Length]++;

                reallocationCount++;
            }

            return reallocationCount.ToString();
        }

        public string Part2(params string[] input)
        {
            var slots = input[0].Split('\t').Select(int.Parse).ToArray();

            var reallocationCount = 0;
            var allocations = new Dictionary<string, int>();

            while (!allocations.ContainsKey(string.Join(",", slots)))
            {
                allocations.Add(string.Join(",", slots), reallocationCount);

                var maxIndex = slots.Select((value, index) => new { Value = value, Index = index })
                    .GroupBy(_ => _.Value)
                    .OrderByDescending(grp => grp.Key)
                    .First().First().Index;

                var memory = slots[maxIndex];
                slots[maxIndex] = 0;

                for (var i = 0; i < memory; i++)
                    slots[(maxIndex + 1 + i) % slots.Length]++;

                reallocationCount++;
            }

            return (reallocationCount - allocations[string.Join(",", slots)]).ToString();
            
        }
    }
}
