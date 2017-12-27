using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._23
{
    public class Solution : ISolution
    {
        private readonly Regex CmdRegex = new Regex(@"^(\w{3})\s((\w)|(-?\d+))(\s((\w)|(-?\d+)))?$", RegexOptions.Compiled);

        public string Part1(params string[] input)
        {
            var match = CmdRegex.Match(input[0]);
            var y = match.Groups[6].Value;

            return Math.Pow(int.Parse(y) - 2,2).ToString(CultureInfo.InvariantCulture);
        }

        public string Part2(params string[] input)
        {
            var match = CmdRegex.Match(input[0]);
            var y = match.Groups[6].Value;
            
            var i = int.Parse(y) * 100 + 100000;
            
            return Enumerable.Range(1, 1001).Select(_ => 17 * _ + i).Count(_ => !IsPrime(_)).ToString();
        }
        
        public static bool IsPrime(int number)
        {

            if (number == 1) return false;
            if (number == 2) return true;

            for (var i = 2; i <= Math.Ceiling(Math.Sqrt(number)); ++i)
                if (number % i == 0) return false;

            return true;
        }
    }
}
