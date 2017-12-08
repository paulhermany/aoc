using System;
using System.Linq;

namespace Hermany.AoC._2017._08
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var instructions = input.Select(line => Instruction.Parse(line)).ToList();

            var registers = instructions.Select(instruction => instruction.Name).Distinct()
                .ToDictionary(name => name, name => 0);
            
            foreach (var instruction in instructions)
            {
                if (Compare(instruction.Operator, registers[instruction.Lhs], instruction.Rhs))
                    registers[instruction.Name] += instruction.Inc * instruction.Amount;
            }

            return registers.Values.Max().ToString();
        }

        public string Part2(params string[] input)
        {
            var instructions = input.Select(line => Instruction.Parse(line)).ToList();

            var registers = instructions.Select(instruction => instruction.Name).Distinct()
                .ToDictionary(name => name, name => 0);

            var maxValue = 0;

            foreach (var instruction in instructions)
            {
                if (Compare(instruction.Operator, registers[instruction.Lhs], instruction.Rhs))
                    registers[instruction.Name] += instruction.Inc * instruction.Amount;

                if (registers[instruction.Name] > maxValue)
                    maxValue = registers[instruction.Name];
            }

            return maxValue.ToString();
        }

        public static bool Compare(string @operator, int lhs, int rhs)
        {
            switch (@operator)
            {
                case ">":
                    return lhs > rhs;
                case "<":
                    return lhs < rhs;
                case ">=":
                    return lhs >= rhs;
                case "<=":
                    return lhs <= rhs;
                case "==":
                    return lhs == rhs;
                case "!=":
                    return lhs != rhs;
                default:
                    throw new ArgumentException(nameof(@operator));
            }
        }

    }
}
