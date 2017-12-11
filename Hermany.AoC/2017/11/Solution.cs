using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._11
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var directions = input[0].Split(',');
            
            var x = 0;
            var y = 0;
            var z = 0;

            foreach (var direction in directions)
                Step(direction, ref x, ref y, ref z);

            return ((Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2).ToString();
        }

        public string Part2(params string[] input)
        {
            var directions = input[0].Split(',');
            
            var x = 0;
            var y = 0;
            var z = 0;

            var maxDistance = 0;

            foreach (var direction in directions)
            {
                Step(direction, ref x, ref y, ref z);

                var distance = ((Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2);
                if (distance > maxDistance) maxDistance = distance;
            }

            return maxDistance.ToString();
        }

        private void Step(string direction, ref int x, ref int y, ref int z)
        {
            switch (direction)
            {
                case "nw":
                    y++;
                    x--;
                    break;
                case "n":
                    y++;
                    z--;
                    break;
                case "ne":
                    x++;
                    z--;
                    break;
                case "sw":
                    x--;
                    z++;
                    break;
                case "s":
                    y--;
                    z++;
                    break;
                case "se":
                    y--;
                    x++;
                    break;
            }
        }

    }
}
