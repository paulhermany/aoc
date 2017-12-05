using System.Linq;

namespace Hermany.AoC._2017._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var instructions = input.Select(int.Parse).ToArray();
            
            var index = 0;

            var steps = 0;

            while (index >= 0 && index < instructions.Length)
            {
                var currentInstruction = instructions[index];

                instructions[index]++;
                index += currentInstruction;

                steps++;
            }

            return steps.ToString();
        }

        public string Part2(params string[] input)
        {
            return string.Empty;
        }
    }
}
