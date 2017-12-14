using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._14
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var day10Solution = new _2017._10.Solution();

            var count = 0;
            
            for (var i = 0; i < 128; i++)
                count += HexToBinary(day10Solution.Part2($"{input[0]}-{i}"))
                    .Count(_ => _ == '1');

            return count.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var day10Solution = new _2017._10.Solution();

            var grid = new string[128];

            for (var i = 0; i < 128; i++)
                grid[i] = HexToBinary(day10Solution.Part2($"{input[0]}-{i}"));

            var regionIndex = 0;
            var regions = new Dictionary<string, int>();

            for (var r = 0; r < 128; r++)
            {
                for (var c = 0; c < 128; c++)
                {
                    if (regions.ContainsKey($"{r},{c}") || grid[r][c] == '0') continue;
                    ExpandRegion(regions, grid, r, c, regionIndex++);
                }
            }

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
