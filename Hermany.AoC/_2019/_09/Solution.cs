using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._09
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var boost = new Intcode(program);
            var output = boost.Run(1);

            return output.ToString();

        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();


            var boost = new Intcode(program);
            var output = boost.Run(2);

            return output.ToString();

        }
    }
}
