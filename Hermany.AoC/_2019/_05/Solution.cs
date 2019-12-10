using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var intcode = new Intcode(program);
            intcode.Run(1);

            return intcode.Output.Last().ToString();
        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var intcode = new Intcode(program);
            intcode.Run(5);

            return intcode.Output.Last().ToString();
        }
    }
}
