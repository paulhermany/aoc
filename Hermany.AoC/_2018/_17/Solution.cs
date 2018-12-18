using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._17
{
    public class Solution : ISolution
    {
        private const bool Visual = true;
        private const int Framerate = 60;
        private const int WindowWidth = 40;
        private const int WindowHeight = 24;

        private readonly (int, int) _springLocation = (500, 0);

        public string Part1(params string[] input)
        {
            var scan = ParseInput(input);

            var minY = scan.Min(_ => _.Key.Item2);
            var maxY = scan.Max(_ => _.Key.Item2);
            
            Flow(scan, _springLocation);

            // count the number of flowing and still water locations within the Y range
            var flowingWater = scan.Where(_ => _.Key.Item2 >= minY && _.Key.Item2 <= maxY).Count(_ => _.Value == '|');
            var stillWater = scan.Where(_ => _.Key.Item2 >= minY && _.Key.Item2 <= maxY).Count(_ => _.Value == '~');
            var totalWater = flowingWater + stillWater;

            return totalWater.ToString();
        }

        public string Part2(params string[] input)
        {
            var scan = ParseInput(input);

            var minY = scan.Min(_ => _.Key.Item2);
            var maxY = scan.Max(_ => _.Key.Item2);

            Flow(scan, _springLocation);

            // count the number of still water locations within the Y range
            var stillWater = scan.Where(_ => _.Key.Item2 >= minY && _.Key.Item2 <= maxY).Count(_ => _.Value == '~');

            return stillWater.ToString();
        }

        public void Flow(IDictionary<(int, int), char> scan, (int, int) springLocation)
        {
            var maxY = scan.Max(_ => _.Key.Item2);

            var stack = new Stack<(int, int)>();

            scan[springLocation] = '+';
            stack.Push(springLocation);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (Visual)
                {
                    Print(scan, current.Item1 - WindowWidth / 2, current.Item2 - WindowHeight / 2 > 0 ? current.Item2 - WindowHeight / 2 : 0, WindowWidth, WindowHeight);
                    System.Threading.Thread.Sleep(Framerate);
                }

                if (current.Item2 >= maxY) continue;

                var down = (current.Item1, current.Item2 + 1);

                // if the next location down from the current location is empty, water flows down
                if (!scan.ContainsKey(down))
                {
                    scan[down] = '|';
                    stack.Push(down);
                    continue;
                }

                // if the next location down from the current location is flowing water, check for a basin
                //   the IsBasin method will convert all flowing water to still water if it is a basin
                if (scan[down] == '|')
                    CheckForBasin(scan, down);


                // if the location immediately up from the current location is flowing water, add the current location to the stack
                //   the location will be checked after the left and right locations are checked
                var up = (current.Item1, current.Item2 - 1);
                if (scan.ContainsKey(up) && scan[up] == '|' || scan.ContainsKey(up) && scan[up] == '+')
                    stack.Push(up);

                // if the location immediately down from the current location is still water or clay...
                if (scan[down] == '~' || scan[down] == '#')
                {
                    // water can flow to the left
                    var left = (current.Item1 - 1, current.Item2);
                    if (!scan.ContainsKey(left))
                    {
                        scan[left] = '|';
                        stack.Push(left);
                    }

                    // water can flow to the right
                    var right = (current.Item1 + 1, current.Item2);
                    if (!scan.ContainsKey(right))
                    {
                        scan[right] = '|';
                        stack.Push(right);
                    }
                }
            }
        }

        public bool CheckForBasin(IDictionary<(int, int), char> scan, (int, int) start)
        {
            var x = start.Item1;
            var y = start.Item2;

            var minX = x;
            var maxX = x;

            while (true)
            {
                minX = x;
                if (!scan.ContainsKey((x, y))) return false;

                if (scan[(x, y)] == '#') break;

                var down = (x, y + 1);
                if (!scan.ContainsKey(down) || scan[down] == '|') return false;
                
                x--;
            }

            x = maxX;

            while (true)
            {
                maxX = x;
                if (!scan.ContainsKey((x, y))) return false;

                if (scan[(x, y)] == '#') break;

                var down = (x, y + 1);
                if (!scan.ContainsKey(down) || scan[down] == '|') return false;

                x++;
            }

            for (x = minX + 1; x < maxX; x++)
            {
                scan[(x, y)] = '~';
            }

            return true;
        }

        public IDictionary<(int, int), char> ParseInput(string[] input)
        {
            var dict = new Dictionary<(int, int), char>();
            var parser = new Regex(@"([xy])=(\d+),\s[xy]=(\d+)\.\.(\d+)", RegexOptions.Compiled);

            foreach (var line in input)
            {
                var match = parser.Match(line);
                var a = int.Parse(match.Groups[2].Value);
                var minB = int.Parse(match.Groups[3].Value);
                var maxB = int.Parse(match.Groups[4].Value);
                
                for (var b = minB; b <= maxB; b++)
                {
                    if (match.Groups[1].Value == "x")
                        dict[(a, b)] = '#';
                    else
                        dict[(b, a)] = '#';
                }
            }

            return dict;
        }

        public static void Print(IDictionary<(int, int), char> scan, int minX, int minY, int width, int height)
        {
            var cursorX = 0;
            var cursorY = 0;
            for (var y = minY; y < minY + height; y++)
            {
                for (var x = minX; x < minX + width; x++)
                {
                    var pos = (x, y);
                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.CursorVisible = false;
                    Console.Write(scan.ContainsKey(pos) ? scan[pos] : ' ');
                    cursorX++;
                }
                Console.WriteLine();
                cursorY++;
                cursorX = 0;
            }
        }

        public static string PrintFull(IDictionary<(int, int), char> scan)
        {
           var minX = scan.Min(_ => _.Key.Item1);
           var maxX = scan.Max(_ => _.Key.Item1);
           var minY = scan.Min(_ => _.Key.Item2);
           var maxY = scan.Max(_ => _.Key.Item2);

            var sb = new StringBuilder();

            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    sb.Append(scan.ContainsKey((x, y)) ? scan[(x, y)] : ' ');
                }
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
