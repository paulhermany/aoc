using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._16
{
    public class Solution : ISolution
    {
        public static Dictionary<string, Action<int[], int, int, int>> Instructions = new Dictionary<string, Action<int[], int, int, int>>();

        public string Part1(params string[] input)
        {
            InitializeInstructions();
            
            var testCases = TestCase.ParseInput(input);

            var behavesLike3OrMoreCount = 0;

            foreach (var testCase in testCases)
            {
                var behavesLikeCount = 0;
                foreach (var instruction in Instructions)
                {
                    if (testCase.Run(instruction.Value))
                        behavesLikeCount++;

                    if (behavesLikeCount >= 3)
                    {
                        behavesLike3OrMoreCount++;
                        break;
                    }
                }
            }
            
            return behavesLike3OrMoreCount.ToString();
        }

        public string Part2(params string[] input)
        {
            InitializeInstructions();
            
            var testCases = TestCase.ParseInput(input);

            var opcodeCandidates = new Dictionary<int, HashSet<string>>();

            foreach (var testCase in testCases)
            {
                foreach (var instruction in Instructions)
                {
                    if (testCase.Run(instruction.Value))
                    {
                        if (!opcodeCandidates.ContainsKey(testCase.OpCode))
                            opcodeCandidates.Add(testCase.OpCode, new HashSet<string>());

                        if (!opcodeCandidates[testCase.OpCode].Contains(instruction.Key))
                            opcodeCandidates[testCase.OpCode].Add(instruction.Key);
                    }
                }
            }

            while (opcodeCandidates.Any(_ => _.Value.Count > 1))
            {
                var opcodeIdentified = opcodeCandidates.First(_ => _.Value.Count == 1);
                var opcodeIdentifiedKey = opcodeIdentified.Value.Single();

                foreach (var opcodeCandidate in opcodeCandidates.Where(_ => _.Key != opcodeIdentified.Key && _.Value.Contains(opcodeIdentifiedKey)))
                    opcodeCandidate.Value.Remove(opcodeIdentifiedKey);

                foreach (var opcodeCandidate in opcodeCandidates)
                {
                    var foundName = string.Empty;
                    foreach (var name in opcodeCandidate.Value)
                    {
                        if (opcodeCandidates.Where(_ => _.Key != opcodeCandidate.Key).All(_ => !_.Value.Contains(name)))
                        {
                            foundName = name;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(foundName))
                    {
                        opcodeCandidate.Value.Clear();
                        opcodeCandidate.Value.Add(foundName);
                    }
                }
            }

            var lookup = opcodeCandidates.Select(_ => new
            {
                Code = _.Key,
                Value = _.Value.Single()
            }).ToDictionary(_ => _.Code, _ => _.Value);
            
            var program = input.Skip(3354).Select(_ => _.Split(' ').Select(int.Parse).ToArray());

            var register = new int[] { 0, 0, 0, 0 };

            foreach (var line in program)
            {
                var instruction = Instructions[lookup[line[0]]];
                instruction.Invoke(register, line[1], line[2], line[3]);
            }

            return register[0].ToString();
        }

        public void InitializeInstructions()
        {
            Instructions.Clear();

            //addr (add register) stores into register C the result of adding register A and register B.    
            Instructions.Add("addr", (input, a, b, c) => input[c] = input[a] + input[b]);

            //addi (add immediate) stores into register C the result of adding register A and value B.
            Instructions.Add("addi", (input, a, b, c) => input[c] = input[a] + b);

            //mulr (multiply register) stores into register C the result of multiplying register A and register B.
            Instructions.Add("mulr", (input, a, b, c) => input[c] = input[a] * input[b]);

            //muli (multiply immediate) stores into register C the result of multiplying register A and value B.
            Instructions.Add("muli", (input, a, b, c) => input[c] = input[a] * b);

            //banr (bitwise AND register) stores into register C the result of the bitwise AND of register A and register B.
            Instructions.Add("banr", (input, a, b, c) => input[c] = input[a] & input[b]);

            //bani (bitwise AND immediate) stores into register C the result of the bitwise AND of register A and value B.
            Instructions.Add("bani", (input, a, b, c) => input[c] = input[a] & b);

            //borr (bitwise OR register) stores into register C the result of the bitwise OR of register A and register B.
            Instructions.Add("borr", (input, a, b, c) => input[c] = input[a] | input[b]);

            //bori (bitwise OR immediate) stores into register C the result of the bitwise OR of register A and value B.
            Instructions.Add("bori", (input, a, b, c) => input[c] = input[a] | b);

            //setr (set register) copies the contents of register A into register C. (Input B is ignored.)
            Instructions.Add("setr", (input, a, b, c) => input[c] = input[a]);

            //seti (set immediate) stores value A into register C. (Input B is ignored.)
            Instructions.Add("seti", (input, a, b, c) => input[c] = a);

            //gtir (greater-than immediate/register) sets register C to 1 if value A is greater than register B. Otherwise, register C is set to 0.
            Instructions.Add("gtir", (input, a, b, c) => input[c] = a > input[b] ? 1 : 0);

            //gtri (greater-than register/immediate) sets register C to 1 if register A is greater than value B. Otherwise, register C is set to 0.
            Instructions.Add("gtri", (input, a, b, c) => input[c] = input[a] > b ? 1 : 0);

            //gtrr (greater-than register/register) sets register C to 1 if register A is greater than register B. Otherwise, register C is set to 0.
            Instructions.Add("gtrr", (input, a, b, c) => input[c] = input[a] > input[b] ? 1 : 0);

            //eqir (equal immediate/register) sets register C to 1 if value A is equal to register B. Otherwise, register C is set to 0.
            Instructions.Add("eqir", (input, a, b, c) => input[c] = a == input[b] ? 1 : 0);

            //eqri (equal register/immediate) sets register C to 1 if register A is equal to value B. Otherwise, register C is set to 0.
            Instructions.Add("eqri", (input, a, b, c) => input[c] = input[a] == b ? 1 : 0);

            //eqrr (equal register/register) sets register C to 1 if register A is equal to register B. Otherwise, register C is set to 0.
            Instructions.Add("eqrr", (input, a, b, c) => input[c] = input[a] == input[b] ? 1 : 0);
        }
    }
    

    public class TestCase
    {
        
        public int OpCode { get; set; }
        public int InputA { get; set; }
        public int InputB { get; set; }
        public int Output { get; set; }

        public int[] Before { get; set; }
        public int[] After { get; set; }

        public bool Run(Action<int[], int, int, int> instruction)
        {
            var before = (int[])Before.Clone();
            instruction.Invoke(before, InputA, InputB, Output);
            return before.SequenceEqual(After);
        }

        public static TestCase Parse(string line1, string line2, string line3)
        {
            var tokens = line2.Split(' ').Select(int.Parse).ToArray();
            return new TestCase
            {
                OpCode = tokens[0],
                InputA = tokens[1],
                InputB = tokens[2],
                Output = tokens[3],
                Before = _parser.Match(line1).Groups.Cast<Group>().Skip(1).Take(4).Select(_ => int.Parse(_.Value)).ToArray(),
                After = _parser.Match(line3).Groups.Cast<Group>().Skip(1).Take(4).Select(_ => int.Parse(_.Value)).ToArray()
            };
        }

        public static IEnumerable<TestCase> ParseInput(string[] input)
        {
            var i = 0;
            while (true)
            {
                if (!input[i].StartsWith("Before:")) break;

                yield return TestCase.Parse(input[i], input[i + 1], input[i + 2]);

                i += 4;
            }
        }

        private static readonly Regex _parser = new Regex(@"\w+:\s+\[(\d+),\s(\d+),\s(\d+),\s(\d+)]", RegexOptions.Compiled);
    }
}
