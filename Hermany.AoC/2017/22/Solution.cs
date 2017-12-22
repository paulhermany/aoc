using System;
using System.Linq;

namespace Hermany.AoC._2017._22
{
    public class Solution : ISolution
    {
        public int NumberOfIterations { get; set; } = 0;
        public string Part1(params string[] input)
        {
            var h = input.Length;
            var w = input[0].Length;

            var infected = input
                .SelectMany(
                    (line, row) => 
                    line.Select(
                        (c, col) => 
                        new {Y = -(row - h / 2), X = col - w / 2, I = c == '#'}
                    )
                )
                .Where(_ => _.I)
                .ToDictionary(_ => $"{_.X},{_.Y}", _ => _.I);

            var x = 0;
            var y = 0;
            var t = Math.PI / 2;

            var infections = 0;

            for (var i = 0; i < NumberOfIterations; i++)
            {
                if (infected.ContainsKey($"{x},{y}"))
                {
                    // turn right
                    t -= Math.PI / 2;

                    // clean
                    infected.Remove($"{x},{y}");
                }
                else
                {
                    // turn left
                    t += Math.PI / 2;

                    // infect
                    infected.Add($"{x},{y}", true);
                    infections++;
                }

                x += (int)Math.Round(Math.Cos(t));
                y += (int)Math.Round(Math.Sin(t));
            }

            return infections.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var h = input.Length;
            var w = input[0].Length;

            var map = input
                .SelectMany(
                    (line, row) =>
                        line.Select(
                            (c, col) =>
                                new { Y = -(row - h / 2), X = col - w / 2, V = c }
                        )
                )
                .ToDictionary(_ => $"{_.X},{_.Y}", _ => _.V);

            var x = 0;
            var y = 0;
            var t = Math.PI / 2;

            var infections = 0;

            for (var i = 0; i < NumberOfIterations; i++)
            {
                
                if (!map.ContainsKey($"{x},{y}"))
                    map.Add($"{x},{y}", '.');

                var currentNode = map[$"{x},{y}"];

                switch (currentNode)
                {
                    case '.':
                        t += Math.PI / 2;
                        map[$"{x},{y}"] = '~';
                        break;
                    case '~':
                        map[$"{x},{y}"] = '#';
                        infections++;
                        break;
                    case '#':
                        t -= Math.PI / 2;
                        map[$"{x},{y}"] = '!';
                        break;
                    case '!':
                        t += Math.PI;
                        map[$"{x},{y}"] = '.';
                        break;
                }
   
                x += (int)Math.Round(Math.Cos(t));
                y += (int)Math.Round(Math.Sin(t));
            }

            return infections.ToString();
        }
    }
}
