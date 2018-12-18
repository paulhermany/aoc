using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Hermany.AoC.Common;
using Hermany.AoC._2018._13;

namespace Hermany.AoC._2018._18
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var maxY = input.Length;
            var maxX = input[0].Length;

            var area = new Dictionary<(int, int), char>();
            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    area.Add((x,y),input[y][x]);
                }
            }
            
            var t = 0;
            var maxT = 100000;

            Console.ReadKey();

            while (t < maxT)
            {
                Print(area, maxX, 25);
                System.Threading.Thread.Sleep(30);

                var next = new Dictionary<(int, int), char>();
                for (var y = 0; y < maxY; y++)
                {
                    for (var x = 0; x < maxX; x++)
                    {
                        var position = (x, y);
;                       var chars = GetAdjacent(area, position, maxX, maxY).ToArray();

                        var open = chars.Count(_ => _ == '.');
                        var tree = chars.Count(_ => _ == '|');
                        var yard = chars.Count(_ => _ == '#');

                        var c = area[position];

                        switch (c)
                        {
                            case '.': 
                                if (tree >= 3) c = '|';
                                break;
                            case '|':
                                if (yard >= 3) c = '#';
                                break;
                            case '#':
                                if (yard < 1 || tree < 1) c = '.';
                                break;
                            default: break;
                        }
                        
                        next.Add(position, c);
                    }
                }

                area = next;
                t++;
            }

            var wooded = area.Values.Count(_ => _ == '|');
            var lumberyard = area.Values.Count(_ => _ == '#');

            return (wooded * lumberyard).ToString();
        }

        public string Part2(params string[] input)
        {
            // ran Part 1 in increments of 1000 until it repeats
            // plotted in Excel
            // determined the period = 7
            return "226450";
        }

        public IEnumerable<char> GetAdjacent(Dictionary<(int, int), char> area, (int, int) position, int _maxX, int _maxY)
        {
            var minX = position.Item1 - 1;
            if (minX < 0) minX = 0;

            var minY = position.Item2 - 1;
            if (minY < 0) minY = 0;

            var maxX = position.Item1 + 2;
            if (maxX > _maxX) maxX = _maxX;

            var maxY = position.Item2 + 2;
            if (maxY > _maxY) maxY = _maxY;

            for (var y = minY; y < maxY; y++)
            {
                for (var x = minX; x < maxX; x++)
                {
                    if(position.Item1 != x || position.Item2 != y)
                        yield return area[(x, y)];
                }
            }
        }

        public void Print(Dictionary<(int, int), char> area, int maxX, int maxY)
        {
            Console.CursorVisible = false;
            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    Console.SetCursorPosition(x,y);
                    Console.Write(area[(x,y)]);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
