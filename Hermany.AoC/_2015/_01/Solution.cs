using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;

namespace Hermany.AoC._2015._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return GetOffset(input.Single()).ToString();
        }

        public string Part2(params string[] input)
        {
            return GetIndexOfFirstNegativeOffset(input.Single()).ToString();
        }
       
        public int GetOffset(string val, char up = '(', char down = ')')
        {
            var counts = val.GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());
            
            var offset = (counts.ContainsKey(up) ? counts[up] : 0) - (counts.ContainsKey(down) ? counts[down] : 0);

            return offset;
        }

        public int GetIndexOfFirstNegativeOffset(string val, char up = '(', char down = ')')
        {
            var f = 0;
            int i;

            for (i = 0; i < val.Length && f >= 0; i++)
            {
                if (val[i] == up) f++;
                else if (val[i] == down) f--;
            }

            return i;
        }
    }
}
