using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Schema;
using Hermany.AoC.Common;
using Priority_Queue;

namespace Hermany.AoC._2018._23
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var bots = ParseBots(input);

            var strongestBot = bots.OrderByDescending(_ => _.R).First();

            var numberOfBotsInRange = bots.Count(_ => GetManhattanDistance(_.P, strongestBot.P) <= strongestBot.R);

            return numberOfBotsInRange.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var bots = ParseBots(input);

            // calculate the minimum and maximum range of all bots
            (long X, long Y, long Z) rangeMin = (bots.Min(_ => _.P.X - _.R), bots.Min(_ => _.P.Y - _.R), bots.Min(_ => _.P.Z - _.R));
            (long X, long Y, long Z) rangeMax = (bots.Max(_ => _.P.X + _.R), bots.Max(_ => _.P.Y + _.R), bots.Max(_ => _.P.Z + _.R));

            // determine the maximum dimension size
            var maxDim = new[] {rangeMax.X - rangeMin.X + 1, rangeMax.Y - rangeMin.Y + 1, rangeMax.Z - rangeMin.Z + 1}.Max();

            // determine the nearest power of 2 greater than or equal to the maximum dimension size
            var pow = Math.Ceiling(Math.Log(maxDim, 2));

            // calculate the size of the starting "box"
            var size = Convert.ToInt64(Math.Pow(2, pow));

            (long, long, long) origin = (0, 0, 0);

            // keep track of the "best" bounding box, which is the box that contains the most number of bots in range
            (long X, long Y, long Z) best = rangeMin;

            // terminates when the size is 1
            while (size > 0)
            {
                var maxBots = 0;

                for (var x = rangeMin.X; x <= rangeMax.X; x += size)
                {
                    for (var y = rangeMin.Y; y <= rangeMax.Y; y += size)
                    {
                        for (var z = rangeMin.Z; z <= rangeMax.Z; z += size)
                        {
                            var current = (x, y, z);
                            var c = GetNumberOfBotsInRange(bots, current, size);

                            if (c > maxBots)
                            {
                                maxBots = c;
                                best = current;
                                continue;
                            }

                            if (c == maxBots)
                            {
                                if (
                                    // if the best box is the origin (not yet set)
                                    best.Equals(origin) 
                                    // or if the current box is closer than the best box to the origin
                                    || GetManhattanDistance(current, origin) < GetManhattanDistance(best, origin))
                                    // then set the best box to the current box
                                    best = current;
                            }


                        }
                    }
                }

                rangeMin = (best.X - size, best.Y - size, best.Z - size);
                rangeMax = (best.X + size, best.Y + size, best.Z + size);

                size = size / 2;
            }

            return GetManhattanDistance(best, origin).ToString();
        }


        private ((long X, long Y, long Z) P, long R)[] ParseBots(string[] input)
        {
            var regex = new Regex(@"^pos=<(-?\d+),(-?\d+),(-?\d+)>,\sr=(\d+)$", RegexOptions.Compiled);

            return input.Select(_ =>
            {
                var match = regex.Match(_);
                return (
                    (
                        long.Parse(match.Groups[1].Value),
                        long.Parse(match.Groups[2].Value),
                        long.Parse(match.Groups[3].Value)
                    ),
                    long.Parse(match.Groups[4].Value)
                );
            }).ToArray();
        }

        private int GetNumberOfBotsInRange(IEnumerable<((long X, long Y, long Z) P, long R)> bots, (long X, long Y, long Z) p, long s)
        {
            return bots.Count(_ => GetManhattanDistance(_.P, p) - _.R < s);
        }

        private long GetManhattanDistance((long X, long Y, long Z) a, (long X, long Y, long Z) b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
        }
    }
}
