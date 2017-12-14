using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2017._14
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var day10Solution = new _2017._10.Solution();

            return Enumerable.Range(0, 128)
                .Sum(i => HexToBinary(day10Solution.Part2($"{input[0]}-{i}")).Count(c => c == '1')).ToString();
        }
        
        public string Part2(params string[] input)
        {
            var day10Solution = new _2017._10.Solution();

            var grid = Enumerable.Range(0, 128)
                .Select(i => HexToBinary(day10Solution.Part2($"{input[0]}-{i}"))).ToArray();
            
            var regionIndex = 0;
            var regions = new Dictionary<string, int>();

            for (var r = 0; r < 128; r++)
                for (var c = 0; c < 128; c++)
                    if (!regions.ContainsKey($"{r},{c}") && grid[r][c] == '1')
                      ExpandRegion(regions, grid, r, c, regionIndex++);

            return regionIndex.ToString();
        }

        private static string HexToBinary(string hex) =>
            string.Concat(hex.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

        private static void ExpandRegion(IDictionary<string, int> regions, IReadOnlyList<string> grid, int r, int c, int regionIndex)
        {
            if (c < 0 || c > 127 || r < 0 || r > 127 || grid[r][c] == '0' || regions.ContainsKey($"{r},{c}"))
                return;
            
            regions.Add($"{r},{c}", regionIndex);

            ExpandRegion(regions, grid, r, c+1, regionIndex);
            ExpandRegion(regions, grid, r, c - 1, regionIndex);
            ExpandRegion(regions, grid, r + 1, c, regionIndex);
            ExpandRegion(regions, grid, r - 1, c, regionIndex);
        }
    }
}
