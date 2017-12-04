using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var vectors = input[0]
                .Split(',')
                .Select(i => i.Trim())
                .Select(i => new {
                    Rotation = i.Substring(0, 1) == "R" ? -Math.PI / 2 : Math.PI / 2,
                    Distance = Convert.ToInt32(i.Substring(1))
                });

            var rotation = Math.PI / 2;

            var x = 0;
            var y = 0;
            
            foreach (var vector in vectors)
            {
                rotation += vector.Rotation;

                x += vector.Distance * (int)Math.Round(Math.Cos(rotation));
                y += vector.Distance * (int)Math.Round(Math.Sin(rotation));
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public string Part2(params string[] input)
        {
            var vectors = input[0]
                .Split(',')
                .Select(i => i.Trim())
                .Select(i => new {
                    Rotation = i.Substring(0, 1) == "R" ? -Math.PI / 2 : Math.PI / 2,
                    Distance = Convert.ToInt32(i.Substring(1))
                });

            var rotation = Math.PI / 2;

            var x = 0;
            var y = 0;

            var visited = new Dictionary<string, int>();

            foreach (var vector in vectors)
            {
                rotation += vector.Rotation;

                for (var d = 1; d <= vector.Distance; d++)
                {
                    x += (int)Math.Round(Math.Cos(rotation));
                    y += (int)Math.Round(Math.Sin(rotation));

                    var position = $"{x},{y}";

                    if (visited.ContainsKey(position))
                        return (Math.Abs(x) + Math.Abs(y)).ToString();

                    visited.Add(position, 0);
                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }
    }
}
