using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Hermany.AoC._2017._19
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var y = 0;
            var x = input[0].IndexOf('|');

            var t = 3 * Math.PI / 2;

            var sb = new StringBuilder();

            while (true)
            {
                var c = input[-1 * y][x];
                if (c >= 65 && c <= 90) sb.Append(c);

                var _t = t;
                var _x = x + (int)Math.Round(Math.Cos(_t));
                var _y = y + (int)Math.Round(Math.Sin(_t));

                var isEmpty = (Func<int, int, bool>)((row, col) => col >= 0 && col < input.Length && row >= 0 && row < input[col].Length && input[col][row] != ' ');

                if (isEmpty(_x, -1 * _y))
                {
                    x = _x;
                    y = _y;
                    continue;
                }

                // left turn
                _t = t + Math.PI / 2;
                _x = x + (int)Math.Round(Math.Cos(_t));
                _y = y + (int)Math.Round(Math.Sin(_t));


                if (isEmpty(_x, -1 * _y))
                {
                    t = _t;
                    x = _x;
                    y = _y;
                    continue;
                }

                // right turn
                _t = t - Math.PI / 2;
                _x = x + (int)Math.Round(Math.Cos(_t));
                _y = y + (int)Math.Round(Math.Sin(_t));

                if (isEmpty(_x, -1 * _y))
                {
                    t = _t;
                    x = _x;
                    y = _y;
                    continue;
                }

                break;
            }

            return sb.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var y = 0;
            var x = input[0].IndexOf('|');

            var t = 3 * Math.PI / 2;

            var steps = 0;

            var sb = new StringBuilder();

            while (true)
            {
                steps++;

                var c = input[-1 * y][x];
                if (c >= 65 && c <= 90) sb.Append(c);

                var _t = t;
                var _x = x + (int)Math.Round(Math.Cos(_t));
                var _y = y + (int)Math.Round(Math.Sin(_t));

                var isEmpty = (Func<int, int, bool>)((row, col) => col >= 0 && col < input.Length && row >= 0 && row < input[col].Length && input[col][row] != ' ');

                if(isEmpty(_x, -1 * _y))
                {
                    x = _x;
                    y = _y;
                    continue;
                }

                // left turn
                _t = t + Math.PI / 2;
                _x = x + (int)Math.Round(Math.Cos(_t));
                _y = y + (int)Math.Round(Math.Sin(_t));

                
                if (isEmpty(_x, -1 * _y))
                {
                    t = _t;
                    x = _x;
                    y = _y;
                    continue;
                }

                // right turn
                _t = t - Math.PI / 2;
                _x = x + (int)Math.Round(Math.Cos(_t));
                _y = y + (int)Math.Round(Math.Sin(_t));

                if (isEmpty(_x, -1 * _y))
                {
                    t = _t;
                    x = _x;
                    y = _y;
                    continue;
                }

                break;
            }

            return steps.ToString();
        }
    }
}
