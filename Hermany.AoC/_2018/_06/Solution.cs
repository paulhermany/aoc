using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._06
{

    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            // parse the points input into a dictionary of tuples and a generated id
            var points = ParsePoints(input);

            // determine the extent of the "infinite" field
            var minX = points.Keys.Min(_ => _.Item1);
            var maxX = points.Keys.Max(_ => _.Item1);
            var minY = points.Keys.Min(_ => _.Item2);
            var maxY = points.Keys.Max(_ => _.Item2);

            // create a new grid to represent the field of possible coordinates
            var grid = new int[maxX - minX + 1, maxY - minY + 1];

            // iterate through each (x,y) coordinate in the grid
            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    // convert the coordinate to a grid index
                    var gridX = x + (0 - minX);
                    var gridY = y + (0 - minY);

                    // set the minimum distance to by the maximum distance within the field
                    var minDistance = (maxX - minX + 1) + (maxY - minY + 1);

                    foreach (var p in points)
                    {
                        // calculate the manhattan distance for each point relative to the current coordinate
                        var d = ManhattanDistance(p.Key, (x, y));
                       
                        // if the distance matches the minimum distance, that means there is a collision
                        //   assign all collisions a zero index
                        if (d == minDistance)
                            grid[gridX, gridY] = 0;

                        // if the distance is greater than or equal to the minimum distance found so far, skip to the next point
                        if (d >= minDistance) continue;

                        // the distance is less than the minimum distance found so far,
                        //  assign the current point to the current coordinate and set the minimum distance
                        grid[gridX, gridY] = p.Value;
                        minDistance = d;
                    }
                }
            }
            
            // iterate through all coordinates again
            //  - count the number of coordinates claimed per point
            //  - keep track of coordinates that are on the border, these are excluded (infinite area)
            var borderedRegionIds = new HashSet<int>();
            var areas = new Dictionary<int, int>();

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    var gridX = x + (0 - minX);
                    var gridY = y + (0 - minY);

                    // add claimed coordinate to area
                    if (!areas.ContainsKey(grid[gridX, gridY]))
                        areas.Add(grid[gridX, gridY], 0);
                    areas[grid[gridX, gridY]]++;

                    // check for border
                    if (x == minX || x == maxX || y == minY || y == maxY)
                    {
                        if (!borderedRegionIds.Contains(grid[gridX, gridY]))
                            borderedRegionIds.Add(grid[gridX, gridY]);
                    }
                }
            }


            var largestRegion = areas
                // exclude border regions
                .Where(_ => !borderedRegionIds.Contains(_.Key))
                // get maximum area
                .OrderByDescending(_ => _.Value)
                .First();
            
            return largestRegion.Value.ToString();
        }

        public string Part2(params string[] input)
        {
            var points = ParsePoints(input);

            var minX = points.Keys.Min(_ => _.Item1);
            var maxX = points.Keys.Max(_ => _.Item1);
            var minY = points.Keys.Min(_ => _.Item2);
            var maxY = points.Keys.Max(_ => _.Item2);
            
            var size = 0;

            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    // part 2 is a simple sum
                    if (points.Sum(_ => ManhattanDistance((x, y), _.Key)) < 10000) size++;
                }
            }

            return size.ToString();
        }

        private Dictionary<(int, int), int> ParsePoints(string[] input)
        {
            var id = 1;
            return input.Select(_ =>
            {
                var tokens = _.Split(',');
                return new
                {
                    Id = id++,
                    X = int.Parse(tokens[0].Trim()),
                    Y = int.Parse(tokens[1].Trim())
                };
            }).ToDictionary(_ => (_.X, _.Y), _ => _.Id);
        }

        private int ManhattanDistance((int, int) a, (int, int) b)
            => Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
    }
}
