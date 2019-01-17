using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._22
{
    public class Solution : ISolution
    {
        private readonly Dictionary<(long X, long Y), long> _geologicIndexes = new Dictionary<(long X, long Y), long>();
        private readonly Dictionary<(long X, long Y), long> _erosionLevels = new Dictionary<(long X, long Y), long>();
        private readonly Dictionary<char, int> _riskLevels = new Dictionary<char, int>() {{'.', 0}, {'=', 1}, {'|', 2}};

        private (long X, long Y) origin = (X: 0, Y: 0);
        private (long X, long Y) target = (X: 11, Y: 722);
        private long depth = 10689;

        private long xMultiplier = 16807;
        private long yMultiplier = 48271;

        private long modulo = 20183;

        public string Part1(params string[] input)
        {
            var sum = 0;
            
            for (var y = origin.Y; y <= target.Y; y++)
            {
                for (var x = origin.X; x <= target.X; x++)
                {
                    sum += _riskLevels[GetRegionType((x, y))];
                }
            }
            
            return sum.ToString();
        }

        public string Part2(params string[] input)
        {
            // switched from A* to BFS concept from mainhaxor via reddit
            // interesting approach using cycled nodes in queue for tool switch delay
            var minutes = Traverse();

            return minutes.ToString();
        }

        private long GetGeologicIndex((long X, long Y) position)
        {
            if (_geologicIndexes.ContainsKey(position))
                return _geologicIndexes[position];

            long geologicIndex = 0;

            if (position.Equals(origin) || position.Equals(target)) geologicIndex = 0;
            
            else if (position.Y == 0) geologicIndex = position.X * xMultiplier;

            else if (position.X == 0) geologicIndex = position.Y * yMultiplier;
            
            else
                geologicIndex = 
                    GetErosionLevel((position.X - 1, position.Y)) *
                    GetErosionLevel((position.X, position.Y - 1));

            _geologicIndexes.Add(position, geologicIndex);

            return geologicIndex;
        }

        private long GetErosionLevel((long X, long Y) position)
        {
            if (_erosionLevels.ContainsKey(position))
                return _erosionLevels[position];

            var geologicIndex = GetGeologicIndex(position);
            var erosionLevel = (geologicIndex + depth) % modulo;

            _erosionLevels.Add(position, erosionLevel);

            return erosionLevel;
        }

        private char GetRegionType((long X, long Y) position)
        {
            var erosionLevelMod3 = GetErosionLevel(position) % 3;
            switch (erosionLevelMod3)
            {
                case 0: return '.';
                case 1: return '=';
                case 2: return '|';
            }

            return ' ';
        }
        
        private int Traverse()
        {
            (int x, int y)[] delta = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            Queue<((long X, long Y) position, char tool, int delay, int minutes)> queue = new Queue<((long X, long Y), char tool, int switching, int minutes)>();

            HashSet<((long X, long Y), char tool)> seen = new HashSet<((long X, long Y), char tool)>();

            queue.Enqueue((origin, 'T', 0, 0));

            seen.Add((origin, 'T'));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                
                if (current.delay > 0)
                {
                    if (current.delay != 1 || seen.Add((current.position, current.tool)))
                        queue.Enqueue((current.position, current.tool, current.delay - 1, current.minutes + 1));
                    continue;
                }
                
                // exit condition
                if (current.position.Equals(target) && current.tool == 'T')
                    return current.minutes;

                foreach (var (dx, dy) in delta)
                {
                    (long X, long Y) next = (current.position.X + dx, current.position.Y + dy);

                    // stay in bounds
                    if (next.X < 0 || next.Y < 0) continue;

                    if (GetAllowedTools(GetRegionType(next)).Contains(current.tool) && seen.Add((next, current.tool)))
                        queue.Enqueue((next, current.tool, 0, current.minutes + 1));
                }

                foreach (char allowedTool in GetAllowedTools(GetRegionType(current.position)))
                    queue.Enqueue((current.position, allowedTool, 6, current.minutes + 1));
            }

            return 0;
        }
        
        private string GetAllowedTools(char region)
        {
            switch (region)
            {
                case '.': return "CT";
                case '=': return "CN";
                case '|': return "TN";
                default: return "";
            }
        }
    }

    public class Node
    {
        public (int X, int Y) Position { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
    }

}
 
