using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._03
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var wires = input.Select(wire => wire.Split(',').Select(_ => (direction:_[0], length:int.Parse(_.Substring(1)))).ToArray()).ToArray();

            var directions = new Dictionary<char, (int dx, int dy)>
            {
                {'U', (0, 1)},
                {'D', (0, -1)},
                {'R', (1, 0)},
                {'L', (-1, 0)}
            };

            var minDistance = 0;
            var minIntersection = (0, 0);

            var visited = new HashSet<(int,int)>();

            for (var w = 0; w <= 1; w++)
            {
                var wire = wires[w];
                
                var x = 0;
                var y = 0;

                foreach (var bend in wire)
                {

                    for (var i = 0; i < bend.length; i++)
                    {
                        x += directions[bend.direction].dx;
                        y += directions[bend.direction].dy;

                        var point = (x, y);

                        switch (w)
                        {
                            case 0 when !visited.Contains(point):
                                visited.Add(point);
                                break;
                            case 1 when visited.Contains(point):
                            {
                                var distance = ManhattanDistance(point);
                                if (minDistance == 0 || distance < minDistance)
                                {
                                    minDistance = distance;
                                    minIntersection = point;
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return minDistance.ToString();
        }

        public string Part2(params string[] input)
        {
            var wires = input.Select(wire => wire.Split(',').Select(_ => (direction: _[0], length: int.Parse(_.Substring(1)))).ToArray()).ToArray();

            var directions = new Dictionary<char, (int dx, int dy)>
            {
                {'U', (0, 1)},
                {'D', (0, -1)},
                {'R', (1, 0)},
                {'L', (-1, 0)}
            };

            var minSteps = 0;
            
            var visited = new Dictionary<(int, int), (int w1, int w2)>();

            for (var w = 0; w <= 1; w++)
            {
                var wire = wires[w];

                var steps = 0;
                var x = 0;
                var y = 0;

                foreach (var bend in wire)
                {

                    for (var i = 0; i < bend.length; i++)
                    {
                        x += directions[bend.direction].dx;
                        y += directions[bend.direction].dy;
                        steps++;

                        var point = (x, y);

                        switch (w)
                        {
                            case 0 when !visited.ContainsKey(point):
                                visited.Add(point, (steps, 0));
                                break;
                            case 1 when visited.ContainsKey(point):
                            {
                                if (visited[point].w2 == 0)
                                    visited[point] = (visited[point].w1, steps);

                                var stepsToIntersection = visited[point].w1 + visited[point].w2;
                                if (minSteps == 0 || stepsToIntersection < minSteps)
                                {
                                    minSteps = stepsToIntersection;
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return minSteps.ToString();
        }

        private int ManhattanDistance((int x, int y) point) => Math.Abs(point.x) + Math.Abs(point.y);
    }
}
