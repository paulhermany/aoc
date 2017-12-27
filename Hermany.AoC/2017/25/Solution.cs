using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hermany.AoC._2017._25
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var state = 'A';

            var tape = new Dictionary<int, bool>();
            var cursor = 0;

            for (var i = 0; i < 12425180; i++)
            {
                if (!tape.ContainsKey(cursor)) tape.Add(cursor, false);
                
                switch (state)
                {
                    case 'A':
                        if (!tape[cursor])
                        {
                            tape[cursor] = true;
                            cursor++;
                            state = 'B';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor++;
                            state = 'F';
                        }
                        break;
                    case 'B':
                        if (!tape[cursor])
                        {
                            cursor--;
                            state = 'B';
                        }
                        else
                        {
                            cursor--;
                            state = 'C';
                        }
                        break;
                    case 'C':
                        if (!tape[cursor])
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'D';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor++;
                            state = 'C';
                        }
                        break;
                    case 'D':
                        if (!tape[cursor])
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'E';
                        }
                        else
                        {
                            cursor++;
                            state = 'A';
                        }
                        break;
                    case 'E':
                        if (!tape[cursor])
                        {
                            tape[cursor] = true;
                            cursor--;
                            state = 'F';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor--;
                            state = 'D';
                        }
                        break;
                    case 'F':
                        if (!tape[cursor])
                        {
                            tape[cursor] = true;
                            cursor++;
                            state = 'A';
                        }
                        else
                        {
                            tape[cursor] = false;
                            cursor--;
                            state = 'E';
                        }
                        break;
                }
            }
            
            return tape.Count(_ => _.Value).ToString();
        }
        
        public string Part2(params string[] input)
        {
            return string.Empty;
        }
    }
}
