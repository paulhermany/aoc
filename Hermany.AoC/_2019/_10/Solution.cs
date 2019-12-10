using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._10
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var grid = input
                .SelectMany((line, y) => line.Select((c, x) => new { P = (X: x, Y: y), C = c }))
                .ToDictionary(_ => _.P, _ => _.C);

            var otherAsteroids = grid.Keys.ToDictionary(_ => _, _ => 0);

            foreach (var key in grid.Keys)
            {
                if (grid[key] != '#') continue;

                for (var y = 0; y <= grid.Keys.Max(_ => _.Y); y++)
                {
                    for (var x = 0; x <= grid.Keys.Max(_ => _.X); x++)
                    {
                        if (x == key.X && y == key.Y) continue;
                        if (grid[(x, y)] != '#') continue;

                        var dx = Math.Abs(key.X - x);
                        var dy = Math.Abs(key.Y - y);

                        var sx = key.X - x < 0 ? -1 : 1;
                        var sy = key.Y - y < 0 ? -1 : 1;

                        var gcd = GCD(dx, dy);

                        dx = dx / gcd * sx;
                        dy = dy / gcd * sy;

                        var xi = x;
                        var yi = y;

                        bool los = true;

                        while (xi != key.X || yi != key.Y)
                        {
                            xi += dx;
                            yi += dy;

                            if (grid[(xi, yi)] == '#' && (xi != key.X || yi != key.Y)) los = false;
                        }

                        if (los) otherAsteroids[key]++;
                    }
                }
            }

            var maxOther = otherAsteroids.OrderByDescending(_ => _.Value).First();

            return maxOther.ToString();
        }

        public string Part2(params string[] input)
        {
            var origin = (X: 20, Y: 21);
            var maxKillCount = 200;

            var grid = input
                .SelectMany((line, y) => line.Select((c, x) => new {P = (X: x, Y: y), C = c}))
                .ToDictionary(_ => _.P, _ =>
                {
                    var x = _.P.X - origin.X;
                    var y = origin.Y - _.P.Y;

                    var dx = Math.Abs(origin.X - _.P.X);
                    var dy = Math.Abs(origin.Y - _.P.Y);

                    var sx = origin.X - _.P.X < 0 ? -1 : 1;
                    var sy = origin.Y - _.P.Y < 0 ? -1 : 1;

                    return new Point
                    {
                        IsAsteroid = _.C == '#',
                        IsKilled = x ==0 && y == 0,
                        X = x,
                        Y = y,
                        T = GetAngle(x, y),
                        D = Math.Sqrt(Math.Pow(y,2) + Math.Pow(x, 2))
                    };
                });

            var killCount = 0;

            var angles = grid.Values.Where(_ => _.IsAsteroid && !_.IsKilled).Select(_ => _.T).Distinct().OrderByDescending(_ => _).ToArray();
            var angleIndex = Array.IndexOf(angles, Math.PI / 2);

            Point lastKilled = new Point();

            while (grid.Values.Any(_ => _.IsAsteroid && !_.IsKilled) && killCount < maxKillCount)
            {
                var currentAngle = angles[angleIndex];

                var targets = grid.Where(_ =>
                        _.Value.T == currentAngle && _.Value.IsAsteroid && !_.Value.IsKilled)
                    .OrderBy(_ => _.Value.D);

                if (targets.Any())
                {
                    var target = targets.First();
                    target.Value.IsKilled = true;
                    lastKilled = target.Value;
                    killCount++;
                }

                angleIndex = (angleIndex + 1) % angles.Length;
            }

            return ((lastKilled.X + origin.X) * 100 + (origin.Y - lastKilled.Y)).ToString();
        }

        public class Point
        {
            public bool IsAsteroid { get; set; }
            public bool IsKilled { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public double T { get; set; }
            public double D { get; set; }
        }

        private double GetAngle(int x, int y)
        {
            if (x == 0)
            {
                if (y >= 0) return Math.PI / 2;
                return 3 * Math.PI / 2;
            }

            if (x < 0)
                return Math.Atan(y / (double)x) + Math.PI;

            if (y < 0)
                return Math.Atan(y / (double) x) + 2 * Math.PI;

            return Math.Atan(y / (double) x);
        }

        private int GCD(int a, int b)
        {
            while (b > 0)
            {
                var rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

    }
}
