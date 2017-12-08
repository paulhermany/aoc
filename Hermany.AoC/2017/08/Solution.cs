using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2017._08
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var instructions = new List<Instruction>();

            foreach(var line in input)
                instructions.Add(Instruction.Parse(line));

            var registers = new Dictionary<string, int>();
            
            foreach(var name in instructions.Select(_ => _.Name).Distinct())
                registers.Add(name, 0);
            
            foreach (var instruction in instructions)
            {
                var lhs = registers[instruction.Lhs];
                var shouldModify = false;
                switch (instruction.Operator)
                {
                    case ">":
                        shouldModify = lhs > instruction.Rhs;
                        break;
                    case "<":
                        shouldModify = lhs < instruction.Rhs;
                        break;
                    case ">=":
                        shouldModify = lhs >= instruction.Rhs;
                        break;
                    case "<=":
                        shouldModify = lhs <= instruction.Rhs;
                        break;
                    case "==":
                        shouldModify = lhs == instruction.Rhs;
                        break;
                    case "!=":
                        shouldModify = lhs != instruction.Rhs;
                        break;
                }

                if (shouldModify)
                    registers[instruction.Name] += instruction.Inc * instruction.Amount;
            }

            return registers.Values.Max().ToString();
        }

        public string Part2(params string[] input)
        {
            var instructions = new List<Instruction>();

            foreach (var line in input)
                instructions.Add(Instruction.Parse(line));

            var registers = new Dictionary<string, int>();

            foreach (var name in instructions.Select(_ => _.Name).Distinct())
                registers.Add(name, 0);

            var maxValue = 0;

            foreach (var instruction in instructions)
            {
                var lhs = registers[instruction.Lhs];
                var shouldModify = false;
                switch (instruction.Operator)
                {
                    case ">":
                        shouldModify = lhs > instruction.Rhs;
                        break;
                    case "<":
                        shouldModify = lhs < instruction.Rhs;
                        break;
                    case ">=":
                        shouldModify = lhs >= instruction.Rhs;
                        break;
                    case "<=":
                        shouldModify = lhs <= instruction.Rhs;
                        break;
                    case "==":
                        shouldModify = lhs == instruction.Rhs;
                        break;
                    case "!=":
                        shouldModify = lhs != instruction.Rhs;
                        break;
                }

                if (shouldModify)
                    registers[instruction.Name] += instruction.Inc * instruction.Amount;

                if (registers[instruction.Name] > maxValue)
                    maxValue = registers[instruction.Name];
            }

            return maxValue.ToString();
        }

    }
}
