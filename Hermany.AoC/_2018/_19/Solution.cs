using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Markup;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._19
{
    public class Solution : ISolution
    {
        public static Dictionary<string, Action<long[], long, long, long>> Instructions = new Dictionary<string, Action<long[], long, long, long>>();
        
        public static Dictionary<string, Func<long, long, long, string>> DisassembledInstructions = new Dictionary<string, Func<long, long, long, string>>();

        public string Part1(params string[] input)
        {
            InitializeInstructions();

            var binding = long.Parse(input[0].Split(' ')[1]);

            var program = input.Skip(1).Select(_ =>
            {
                var tokens = _.Split(' ');
                return new
                {
                    Instruction = tokens[0],
                    A = long.Parse(tokens[1]),
                    B = long.Parse(tokens[2]),
                    C = long.Parse(tokens[3])
                };
            }).ToArray();

            var registers = new long[6];
            
            while (registers[binding] >= 0 && registers[binding] < program.Length)
            {
                var ip = registers[binding];

                var values = program[ip];
                Instructions[program[ip].Instruction].Invoke(registers, values.A, values.B, values.C);
                
                registers[binding]++;
            }

            return registers[0].ToString();
        }

        public string Part2(params string[] input)
        {
            InitializeInstructions();

            var binding = long.Parse(input[0].Split(' ')[1]);

            var program = input.Skip(1).Select(_ =>
            {
                var tokens = _.Split(' ');
                return new
                {
                    Instruction = tokens[0],
                    A = long.Parse(tokens[1]),
                    B = long.Parse(tokens[2]),
                    C = long.Parse(tokens[3])
                };
            }).ToArray();
            
            //Console.WriteLine($"ip = {binding}");
            //var lineNumber = 0;
            //foreach (var line in program)
            //{
            //    Console.Write($"{lineNumber++}:\t");
            //    Console.WriteLine(DisassembledInstructions[line.Instruction].Invoke(line.A, line.B, line.C));
            //}
            //Console.ReadKey();

            var R = new long[6];
            R[0] = 1;

            R[1] = R[1] + 16;
            R[1]++;
            R[4] = R[4] + 2;
            R[1]++;
            R[4] = R[4] * R[4];
            R[1]++;
            R[4] = R[1] * R[4];
            R[1]++;
            R[4] = R[4] * 11;
            R[1]++;
            R[3] = R[3] + 3;
            R[1]++;
            R[3] = R[3] * R[1];
            R[1]++;
            R[3] = R[3] + 4;
            R[1]++;
            R[4] = R[4] + R[3];
            R[1]++;
            R[1] = R[1] + R[0];
            R[1]++;
            R[3] = R[1];
            R[1]++;
            R[3] = R[3] * R[1];
            R[1]++;
            R[3] = R[1] + R[3];
            R[1]++;
            R[3] = R[1] * R[3];
            R[1]++;
            R[3] = R[3] * 14;
            R[1]++;
            R[3] = R[3] * R[1];
            R[1]++;
            R[4] = R[4] + R[3];
            R[1]++;
            R[0] = 0;
            R[1]++;
            R[1] = 0;
            R[1]++;
            
            var N = (int)R[4];

            return Enumerable.Range(1, N).Where(_ => N % _ == 0).Sum().ToString();
        }

        public void InitializeInstructions()
        {
            Instructions.Clear();
            DisassembledInstructions.Clear();

            //addr (add register) stores into register C the result of adding register A and register B.    
            Instructions.Add("addr", (input, a, b, c) => input[c] = input[a] + input[b]);
            DisassembledInstructions.Add("addr", (a,b,c) => $"R[{c}] = R[{a}] + R[{b}]");

            //addi (add immediate) stores into register C the result of adding register A and value B.
            Instructions.Add("addi", (input, a, b, c) => input[c] = input[a] + b);
            DisassembledInstructions.Add("addi", (a, b, c) => $"R[{c}] = R[{a}] + {b}");

            //mulr (multiply register) stores into register C the result of multiplying register A and register B.
            Instructions.Add("mulr", (input, a, b, c) => input[c] = input[a] * input[b]);
            DisassembledInstructions.Add("mulr", (a, b, c) => $"R[{c}] = R[{a}] * R[{b}]");

            //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
            Instructions.Add("muli", (input, a, b, c) => input[c] = input[a] * b);
            DisassembledInstructions.Add("muli", (a, b, c) => $"R[{c}] = R[{a}] * {b}");

            //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
            Instructions.Add("banr", (input, a, b, c) => input[c] = input[a] & input[b]);
            DisassembledInstructions.Add("banr", (a, b, c) => $"R[{c}] = R[{a}] & R[{b}]");

            //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
            Instructions.Add("bani", (input, a, b, c) => input[c] = input[a] & b);
            DisassembledInstructions.Add("bani", (a, b, c) => $"R[{c}] = R[{a}] & {b}");

            //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
            Instructions.Add("borr", (input, a, b, c) => input[c] = input[a] | input[b]);
            DisassembledInstructions.Add("borr", (a, b, c) => $"R[{c}] = R[{a}] | R[{b}]");

            //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
            Instructions.Add("bori", (input, a, b, c) => input[c] = input[a] | b);
            DisassembledInstructions.Add("bori", (a, b, c) => $"R[{c}] = R[{a}] | {b}");

            //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
            Instructions.Add("setr", (input, a, b, c) => input[c] = input[a]);
            DisassembledInstructions.Add("setr", (a, b, c) => $"R[{c}] = R[{a}]");

            //seti (set immediate) stores value A into register C. (Input B is ignored.)
            Instructions.Add("seti", (input, a, b, c) => input[c] = a);
            DisassembledInstructions.Add("seti", (a, b, c) => $"R[{c}] = {a}");

            //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
            Instructions.Add("gtir", (input, a, b, c) => input[c] = a > input[b] ? 1 : 0);
            DisassembledInstructions.Add("gtir", (a, b, c) => $"R[{c}] = a ? R[{b} ? 1 : 0");

            //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
            Instructions.Add("gtri", (input, a, b, c) => input[c] = input[a] > b ? 1 : 0);
            DisassembledInstructions.Add("gtri", (a, b, c) => $"R[{c}] = R[{a}] > {b} ? 1 : 0");

            //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
            Instructions.Add("gtrr", (input, a, b, c) => input[c] = input[a] > input[b] ? 1 : 0);
            DisassembledInstructions.Add("gtrr", (a, b, c) => $"R[{c}] = R[{a}] > R[{b}] ? 1 : 0");

            //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
            Instructions.Add("eqir", (input, a, b, c) => input[c] = a == input[b] ? 1 : 0);
            DisassembledInstructions.Add("eqir", (a, b, c) => $"R[{c}] = {a} == R[{b}] ? 1 : 0");

            //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
            Instructions.Add("eqri", (input, a, b, c) => input[c] = input[a] == b ? 1 : 0);
            DisassembledInstructions.Add("eqri", (a, b, c) => $"R[{c}] = R[{a}] == {b} ? 1 : 0");

            //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.
            Instructions.Add("eqrr", (input, a, b, c) => input[c] = input[a] == input[b] ? 1 : 0);
            DisassembledInstructions.Add("eqrr", (a, b, c) => $"R[{c}] = R[{a}] == R[{b}] ? 1 : 0");
        }
        
    }
}
