using System;
using System.CodeDom;
using System.Collections.Generic;
using Hermany.AoC.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Hermany.AoC.Day03
{
    public class Day03
    {
        public string Part1(string[] input)
        {
            var index = int.Parse(input[0]);

            var x = 0;
            var y = 0;
            
            var dir = 2 * Math.PI;

            var visited = new Dictionary<string, int> {{"0,0", 1}};

            for (var i = 2; i <= index; i++)
            {
                x += (int)Math.Round(Math.Cos(dir));
                y += (int)Math.Round(Math.Sin(dir));
                
                visited.Add($"{x},{y}", i);
                
                var newX = x + (int) Math.Round(Math.Cos(dir + Math.PI / 2));
                var newY = y + (int) Math.Round(Math.Sin(dir + Math.PI / 2));
                
                if (!visited.ContainsKey($"{newX},{newY}"))
                    dir += Math.PI / 2;
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public string Part2(string[] input)
        {
            var index = int.Parse(input[0]);

            var x = 0;
            var y = 0;

            var dir = 2 * Math.PI;

            var visited = new Dictionary<string, int> { { "0,0", 1 } };

            var adjacentSum = 0;

            while(adjacentSum < index)
            {
                x += (int)Math.Round(Math.Cos(dir));
                y += (int)Math.Round(Math.Sin(dir));

                adjacentSum = AdjacentSum(visited, x, y);
                visited.Add($"{x},{y}", adjacentSum);

                var newX = x + (int)Math.Round(Math.Cos(dir + Math.PI / 2));
                var newY = y + (int)Math.Round(Math.Sin(dir + Math.PI / 2));

                if (!visited.ContainsKey($"{newX},{newY}"))
                    dir += Math.PI / 2;
            }

            return adjacentSum.ToString();
        }

        private int AdjacentSum(Dictionary<string, int> values, int x, int y)
        {
            var sum = 0;
            if (values.ContainsKey($"{x - 1},{y}")) sum += values[$"{x - 1},{y}"];
            if (values.ContainsKey($"{x + 1},{y}")) sum += values[$"{x + 1},{y}"];
            if (values.ContainsKey($"{x},{y - 1}")) sum += values[$"{x},{y - 1}"];
            if (values.ContainsKey($"{x},{y + 1}")) sum += values[$"{x},{y + 1}"];
            if (values.ContainsKey($"{x - 1},{y - 1}")) sum += values[$"{x - 1},{y - 1}"];
            if (values.ContainsKey($"{x + 1},{y + 1}")) sum += values[$"{x + 1},{y + 1}"];
            if (values.ContainsKey($"{x - 1},{y + 1}")) sum += values[$"{x - 1},{y + 1}"];
            if (values.ContainsKey($"{x + 1},{y - 1}")) sum += values[$"{x + 1},{y - 1}"];
            return sum;
        }
    }
}
