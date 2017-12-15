using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hermany.AoC._2017._15
{
    public class Solution : ISolution
    {
        private const long MultiplierA = 16807;
        private const long MultiplierB = 48271;
        private const long Divisor = 2147483647;
        
        public string Part1(params string[] input)
        {
            var a = long.Parse(input[0].Substring(24));
            var b = long.Parse(input[1].Substring(24));
            
            var matches = 0;

            for (var i = 0; i < 40000000; i++)
            {
                a = a * MultiplierA % Divisor;
                b = b * MultiplierB % Divisor;

                var abits = Convert.ToString(a, 2);
                var bbits = Convert.ToString(b, 2);

                abits = abits.Substring(abits.Length > 16 ? abits.Length - 16 : 0).PadLeft(16, '0');
                bbits = bbits.Substring(bbits.Length > 16 ? bbits.Length - 16 : 0).PadLeft(16, '0');

                if (abits == bbits)
                    matches++;
            }

            return matches.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var a = long.Parse(input[0].Substring(24));
            var b = long.Parse(input[1].Substring(24));
            
            var matches = 0;

            for (var i = 0; i < 5000000; i++)
            {
                do a = a * MultiplierA % Divisor;
                    while (a % 4 != 0);
                do b = b * MultiplierB % Divisor;
                    while (b % 8 != 0);

                var abits = Convert.ToString(a, 2);
                var bbits = Convert.ToString(b, 2);

                abits = abits.Substring(abits.Length > 16 ? abits.Length - 16 : 0).PadLeft(16, '0');
                bbits = bbits.Substring(bbits.Length > 16 ? bbits.Length - 16 : 0).PadLeft(16, '0');

                if (abits == bbits)
                    matches++;
            }

            return matches.ToString();
        }
    }
}
