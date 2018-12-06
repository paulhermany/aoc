using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._06
{

    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var id = 1;
            var points = input.Select(_ =>
            {
                var tokens = _.Split(',');
                return new
                {
                    Id = id++,
                    X = int.Parse(tokens[0].Trim()),
                    Y = int.Parse(tokens[1].Trim())
                };
            }).ToDictionary(_ => (_.X, _.Y), _ => _.Id);
            
            var minX = points.Keys.Min(_ => _.Item1);
            var maxX = points.Keys.Max(_ => _.Item1);
            var minY = points.Keys.Min(_ => _.Item2);
            var maxY = points.Keys.Max(_ => _.Item2);

            var grid = new int[maxX - minX + 1, maxY - minY + 1];

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var gridX = x + (0 - minX);
                    var gridY = y + (0 - minY);

                    var minDistance = (maxX - minX + 1) + (maxY - minY + 1);

                    foreach (var p in points)
                    {
                        var d = ManhattanDistance(p.Key, (x, y));
                       
                        if (d == minDistance)
                            grid[gridX, gridY] = 0;

                        if (d >= minDistance) continue;

                        grid[gridX, gridY] = p.Value;
                        minDistance = d;
                    }
                }
            }

            var borderedRegionIds = new HashSet<int>();
            var areas = new Dictionary<int, int>();

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var gridX = x + (0 - minX);
                    var gridY = y + (0 - minY);

                    if (!areas.ContainsKey(grid[gridX, gridY]))
                        areas.Add(grid[gridX, gridY], 0);
                    areas[grid[gridX, gridY]]++;

                    if (x == minX || x == maxX || y == minY || y == maxY)
                    {
                        if (!borderedRegionIds.Contains(grid[gridX, gridY]))
                            borderedRegionIds.Add(grid[gridX, gridY]);
                    }
                }
            }

            var largestRegion = areas.Where(_ => !borderedRegionIds.Contains(_.Key)).OrderByDescending(_ => _.Value)
                .First();


            return largestRegion.Value.ToString();
        }

        public string Part2(params string[] input)
        {
            var id = 1;
            var points = input.Select(_ =>
            {
                var tokens = _.Split(',');
                return new
                {
                    Id = id++,
                    X = int.Parse(tokens[0].Trim()),
                    Y = int.Parse(tokens[1].Trim())
                };
            }).ToDictionary(_ => (_.X, _.Y), _ => _.Id);

            var minX = points.Keys.Min(_ => _.Item1);
            var maxX = points.Keys.Max(_ => _.Item1);
            var minY = points.Keys.Min(_ => _.Item2);
            var maxY = points.Keys.Max(_ => _.Item2);
            
            var size = 0;

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    if (points.Sum(_ => ManhattanDistance((x, y), _.Key)) < 10000) size++;
                }
            }

            return size.ToString();
        }

       public static int ManhattanDistance((int, int) a, (int, int) b)
            => Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
    }
}
