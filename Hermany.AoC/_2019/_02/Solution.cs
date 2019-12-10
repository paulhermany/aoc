using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._02
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(int.Parse).ToArray();

            var output = Intcode(program, 12, 2);

            return output[0].ToString();
        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(int.Parse).ToArray();

            var target = 19690720;

            var output = 0;

            var set = PermuteTwo().First(_ => Intcode(program, _.Item1, _.Item2)[0] == target);

            return (100 * set.Item1 + set.Item2).ToString();
        }

        private int[] Intcode(int[] program, int noun, int verb)
        {
            var input = new int[program.Length];
            program.CopyTo(input, 0);

            var position = 0;

            input[1] = noun;
            input[2] = verb;

            int opcode;

            while ((opcode = input[position]) != 99)
            {
                if (position + 3 >= input.Length || input[position + 3] > input.Length)
                {
                    input[0] = 0;
                    return input;
                }

                switch (opcode)
                {
                    case 1:
                        input[input[position + 3]] = input[input[position + 1]] + input[input[position + 2]];
                        break;
                    case 2:
                        input[input[position + 3]] = input[input[position + 1]] * input[input[position + 2]];
                        break;
                }

                position += 4;
            }

            return input;
        }

        private static IEnumerable<(int, int)> PermuteTwo()
        {
            var a = 0;
            var b = 0;

            yield return (a, b);

            while (true)
            {
                a++;

                for (b = 0; b <= a; b++)
                {
                    yield return (a, b);
                    if(b != a)
                        yield return (b, a);
                }
            }
        }
    }
}
