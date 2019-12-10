using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return input.Select(_ => int.Parse(_) / 3 - 2).Sum().ToString();
        }

        public string Part2(params string[] input)
        {
            return input.Select(_ => CalculateRequiredFuel(int.Parse(_))).Sum().ToString();
        }

        private int CalculateRequiredFuel(int mass)
        {
            var requiredFuel = mass / 3 - 2;
            if (requiredFuel <= 0) return 0;
            return requiredFuel + CalculateRequiredFuel(requiredFuel);
        }
    }
}
