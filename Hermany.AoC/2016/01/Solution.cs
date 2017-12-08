using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2016._01
{
    public class Solution : ISolution
    {
        public readonly Dictionary<char, double> Rotations = new Dictionary<char, double>
        {
            {'R', -Math.PI / 2},
            {'L', Math.PI / 2}
        };

        public string Part1(params string[] input)
        {
            var instructions = GetInstructions(input[0]);
            
            var rotation = Math.PI / 2;

            var x = 0;
            var y = 0;
            
            foreach (var instruction in instructions)
            {
                rotation += Rotations[instruction.Rotation];

                x += instruction.Distance * (int)Math.Round(Math.Cos(rotation));
                y += instruction.Distance * (int)Math.Round(Math.Sin(rotation));
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public string Part2(params string[] input)
        {
            var instructions = GetInstructions(input[0]);

            var rotation = Math.PI / 2;

            var x = 0;
            var y = 0;

            var visited = new HashSet<string>();

            foreach (var instruction in instructions)
            {
                rotation += Rotations[instruction.Rotation];

                for (var d = 1; d <= instruction.Distance; d++)
                {
                    x += (int)Math.Round(Math.Cos(rotation));
                    y += (int)Math.Round(Math.Sin(rotation));

                    var position = $"{x},{y}";

                    if (visited.Contains(position))
                        return (Math.Abs(x) + Math.Abs(y)).ToString();

                    visited.Add(position);
                }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        public IEnumerable<Instruction> GetInstructions(string instructions) =>
            instructions
                .Split(',')
                .Select(i => i.Trim())
                .Select(i => new Instruction {
                    Rotation = i[0],
                    Distance = Convert.ToInt32(i.Substring(1))
                });
    }
}
