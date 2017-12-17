using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hermany.AoC._2017._17
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var steps = int.Parse(input[0]);

            var buffer = new List<int> { 0 };

            var index = 0;
            for (var i = 1; i <= 2017; i++)
            {
                index = (index + steps + 1) % buffer.Count;
                if (index == 0)
                {
                    buffer.Add(i);
                    index = buffer.Count - 1;
                }
                else
                {
                    buffer.Insert(index % buffer.Count, i);
                }
            }

            return buffer[(index + 1) % buffer.Count].ToString();
        }
        
        public string Part2(params string[] input)
        {
            var steps = int.Parse(input[0]);

            var bufferCount = 1;

            var valAtZero = 0;

            var index = 0;
            for (var i = 1; i <= 50000000; i++)
            {
                index = (index + steps + 1) % bufferCount;
                if (index == 0)
                    valAtZero = i;

                bufferCount++;
            }

            return valAtZero.ToString();
        }
    }
}
