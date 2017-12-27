using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._18
{
    public class Solution : ISolution
    {
        private readonly Regex CmdRegex = new Regex(@"^(\w{3})\s((\w)|(-?\d+))(\s((\w)|(-?\d+)))?$", RegexOptions.Compiled);

        public string Part1(params string[] input)
        {
            var registers = Enumerable.Range(0, 26)
                .Select(_ => Convert.ToChar(_ + 97))
                .ToDictionary(_ => _, _ => (long)0);
            
            var lastPlayedFrequency = (long)0;
            var recovered = false;
            var index = 0;

            while(index >= 0 && index < input.Length && !recovered) {
                var match = CmdRegex.Match(input[index]);

                var cmd = match.Groups[1].Value;
                var x = match.Groups[2].Value;
                var y = match.Groups[6].Value;

                switch (cmd)
                {
                    case "snd":
                        lastPlayedFrequency = GetValue(registers, x);
                        index++;
                        break;
                    case "set":
                        registers[x[0]] = GetValue(registers, y);
                        index++;
                        break;
                    case "add":
                        registers[x[0]] = registers[x[0]] + GetValue(registers, y);
                        index++;
                        break;
                    case "mul":
                        registers[x[0]] = registers[x[0]] * GetValue(registers, y);
                        index++;
                        break;
                    case "mod":
                        registers[x[0]] = registers[x[0]] % GetValue(registers, y);
                        index++;
                        break;
                    case "rcv":
                        if (GetValue(registers, x) != 0) recovered = true;
                        index++;
                        break;
                    case "jgz":
                        if (GetValue(registers, x) > 0) index += (int) GetValue(registers, y);
                        else index++;
                        break;
                }
            }

            return lastPlayedFrequency.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var programA = new Program(input);
            var programB = new Program(input);

            programA.Registers['p'] = 0;
            programB.Registers['p'] = 1;

            programA.OtherProgram = programB;
            programB.OtherProgram = programA;

            while (!programA.IsWaiting || !programB.IsWaiting)
            {
                programA.Run();
                programB.Run();
            }

            return programB.SendCount.ToString();
        }

        public static long GetValue(IDictionary<char, long> registers, string rhs) =>
            long.TryParse(rhs, out var value) ? value : registers[rhs[0]];
    }
}
