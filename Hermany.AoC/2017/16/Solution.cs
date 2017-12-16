using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hermany.AoC._2017._16
{
    public class Solution : ISolution
    {
        public int NumberOfPrograms { get; set; } = 16;

        public int NumberOfIterations { get; set; } = 1000000000;

        public string Part1(params string[] input)
        {
            var programs = Enumerable.Range(0, NumberOfPrograms)
                .Select(_ => new { Index = _, Value = Convert.ToChar(_ + 97)})
                .ToDictionary(_ => _.Value, _ => _.Index);

            var instructions = input[0].Split(',').Select(_ => new {Code = _[0], Value = _.Substring(1)});

            foreach (var instruction in instructions)
            {
                switch (instruction.Code)
                {
                    case 's':
                        Spin(programs, int.Parse(instruction.Value));
                        break;
                    case 'x':
                        var indicies = instruction.Value.Split('/');
                        Exchange(programs, int.Parse(indicies[0]), int.Parse(indicies[1]));
                        break;
                    case 'p':
                        var keys = instruction.Value.Split('/');
                        Partner(programs, keys[0][0], keys[1][0]);
                        break;
                }
            }

            return string.Concat(programs.OrderBy(_ => _.Value).Select(_ => _.Key).ToArray());
        }
        
        public string Part2(params string[] input)
        {
            var programs = Enumerable.Range(0, NumberOfPrograms)
                .Select(_ => new { Index = _, Value = Convert.ToChar(_ + 97) })
                .ToDictionary(_ => _.Value, _ => _.Index);

            var instructions = input[0].Split(',').Select(_ => new { Code = _[0], Value = _.Substring(1) }).ToArray();

            var iterations = new Dictionary<string, int>();
            var iterationIndex = 0;

            var cycled = false;

            while(!cycled)
            {
                foreach (var instruction in instructions)
                {
                    switch (instruction.Code)
                    {
                        case 's':
                            Spin(programs, int.Parse(instruction.Value));
                            break;
                        case 'x':
                            var indicies = instruction.Value.Split('/');
                            Exchange(programs, int.Parse(indicies[0]), int.Parse(indicies[1]));
                            break;
                        case 'p':
                            var keys = instruction.Value.Split('/');
                            Partner(programs, keys[0][0], keys[1][0]);
                            break;
                    }
                }

                var iteration = string.Concat(programs.OrderBy(_ => _.Value).Select(_ => _.Key).ToArray());

                if (!iterations.ContainsKey(iteration))
                    iterations.Add(iteration, iterationIndex++);
                else
                    cycled = true;
            }

            return iterations.Single(_ => _.Value == NumberOfIterations % iterations.Count - 1).Key;
        }

        private void Spin(Dictionary<char, int> programs, int x)
        {
            var keys = programs.Keys.ToArray();
            foreach (var key in keys)
                programs[key] = (programs[key] + x) % keys.Length;
                //programs[key] = ((programs[key] + x) % keys.Length + keys.Length) % keys.Length;
        }

        private void Exchange(Dictionary<char, int> programs, int a, int b)
        {
            var aEntry = programs.Single(_ => _.Value == a);
            var bEntry = programs.Single(_ => _.Value == b);

            var tmp = aEntry.Value;
            programs[aEntry.Key] = bEntry.Value;
            programs[bEntry.Key] = tmp;
        }

        private void Partner(Dictionary<char, int> programs, char a, char b)
        {
            var tmp = programs[a];
            programs[a] = programs[b];
            programs[b] = tmp;
        }
    }
}
