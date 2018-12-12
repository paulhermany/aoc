using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._12
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var pots = input[0].Split(' ')[2].Select((item, index) => new { Item = item, Index = index })
                .ToDictionary(_ => _.Index, _ => _.Item == '#');

            var generators = input.Skip(3).Select(_ =>
            {
                var tokens =_.Split(new []{"=>"}, StringSplitOptions.None);
                return new
                {
                    Generator = tokens[0].Trim(),
                    Value = tokens[1].Trim() == "#"
                };
            }).ToDictionary(_ => _.Generator, _ => _.Value);

            var generations = 0;
            while (generations < 20)
            {
                var nextState = new Dictionary<int, bool>();

                var plants = pots.Where(_ => _.Value);
                var minPot = plants.Min(_ => _.Key) - 5;
                var maxPot = plants.Max(_ => _.Key) + 5;

                for (var i = minPot; i < maxPot; i++)
                {
                    
                    var generator = GetGenerator(pots, i);

                    if (generators.ContainsKey(generator))
                    {
                        nextState.Add(i, generators[generator]);
                    }
                }

                pots = nextState;
                generations++;
            }

            var sum = pots.Where(_ => _.Value).Sum(_ => _.Key);

            return sum.ToString();
        }

        public string Part2(params string[] input)
        {
            var pots = input[0].Split(' ')[2].Select((item, index) => new { Item = item, Index = index })
                .ToDictionary(_ => _.Index, _ => _.Item == '#');

            var generators = input.Skip(3).Select(_ =>
            {
                var tokens = _.Split(new[] { "=>" }, StringSplitOptions.None);
                return new
                {
                    Generator = tokens[0].Trim(),
                    Value = tokens[1].Trim() == "#"
                };
            }).ToDictionary(_ => _.Generator, _ => _.Value);
            
            // assumptions (based on observation)
            // 1. solution is linear
            // 2. linear pattern begins after (at least) 100 generations
            var lastGeneration = 0;
            var lastSum = 0;
            var lastSlope = 0;
            
            var generations = 0;

            while (true)
            {
                if (generations > 100)
                {
                    var sum = pots.Where(_ => _.Value).Sum(_ => _.Key);
                    var slope = sum - lastSum;
                    lastSum = sum;
                    lastGeneration = generations;

                    if (slope == lastSlope) break;
                    lastSlope = slope;
                }

                var nextState = new Dictionary<int, bool>();

                var plants = pots.Where(_ => _.Value);
                var minPot = plants.Min(_ => _.Key) - 5;
                var maxPot = plants.Max(_ => _.Key) + 5;

                for (var i = minPot; i < maxPot; i++)
                {

                    var generator = GetGenerator(pots, i);

                    if (generators.ContainsKey(generator))
                    {
                        nextState.Add(i, generators[generator]);
                    }
                }

                pots = nextState;
                generations++;
            }
            
            return (50000000000 * lastSlope + (lastSum - lastGeneration * lastSlope)).ToString();
        }

        private string GetGenerator(IDictionary<int, bool> pots, int index)
        {
            var sb = new StringBuilder();
            for (var i = index - 2; i <= index + 2; i++)
            {
                if (pots.ContainsKey(i))
                    sb.Append(pots.ContainsKey(i) ? (pots[i] ? '#' : '.') : '.');
                else
                    sb.Append('.');
            }

            return sb.ToString();
        }

        private string PrintPots(IDictionary<int, bool> pots)
        {
            var sb = new StringBuilder();

            var plants = pots.Where(_ => _.Value).ToArray();
            var minPot = plants.Min(_ => _.Key);
            var maxPot = plants.Max(_ => _.Key);

            for (var i = minPot; i <= maxPot; i++)
            {
                if (pots.ContainsKey(i) && pots[i]) sb.Append('#'); else sb.Append('.');
            }

            return sb.ToString();
        }
    }
}
