using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hermany.AoC._2017._18
{
    public class Program
    {
        private readonly Regex CmdRegex = new Regex(@"^(\w{3})\s((\w)|(-?\d+))(\s((\w)|(-?\d+)))?$", RegexOptions.Compiled);

        public Dictionary<char, long> Registers { get; set; }
        public string[] Commands { get; set; }
        public Queue<long> Values { get; set; }
        public bool IsWaiting { get; set; }
        public Program OtherProgram { get; set; }
        public int SendCount { get; set; }
        public bool IsTerminated { get; set; }

        private long _index;

        public Program(string[] commands)
        {
            Commands = commands;

            IsWaiting = false;
            IsTerminated = false;

            Registers = Enumerable.Range(0, 26)
                .Select(_ => Convert.ToChar(_ + 97))
                .ToDictionary(_ => _, _ => (long)0);

            SendCount = 0;
            
            Values = new Queue<long>();
        }

        public void Run()
        {
            if (IsTerminated) return;
            
            if (_index < 0 || _index >= Commands.Length)
            {
                IsTerminated = true;
                IsWaiting = true;
                return;
            }

            var match = CmdRegex.Match(Commands[_index]);

            var cmd = match.Groups[1].Value;
            var x = match.Groups[2].Value;
            var y = match.Groups[6].Value;

            switch (cmd)
            {
                case "snd":
                    OtherProgram.Values.Enqueue(GetValue(Registers, x));
                    SendCount++;
                    _index++;
                    break;
                case "set":
                    Registers[x[0]] = GetValue(Registers, y);
                    _index++;
                    break;
                case "add":
                    Registers[x[0]] = Registers[x[0]] + GetValue(Registers, y);
                    _index++;
                    break;
                case "mul":
                    Registers[x[0]] = Registers[x[0]] * GetValue(Registers, y);
                    _index++;
                    break;
                case "mod":
                    Registers[x[0]] = Registers[x[0]] % GetValue(Registers, y);
                    _index++;
                    break;
                case "rcv":
                    if (Values.Count == 0)
                    {
                        IsWaiting = true;
                    }
                    else
                    {
                        IsWaiting = false;
                        Registers[x[0]] = Values.Dequeue();
                        _index++;
                    }
                    break;
                case "jgz":
                    if (GetValue(Registers, x) > 0) _index += (int)GetValue(Registers, y);
                    else _index++;
                    break;
            }
        }
        public static long GetValue(IDictionary<char, long> registers, string rhs) =>
            long.TryParse(rhs, out var value) ? value : registers[rhs[0]];
    }
}
