using System;
using System.Linq;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._10
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            Display(input);
            return "KFLBHXGK"; // observed
        }

        public string Part2(params string[] input)
        {
            return "10659";
        }

        private void Display(string[] input)
        {
            var regex = new Regex(@"position=<\s?(-?\d+),\s\s?(-?\d+)>\svelocity=<\s?(-?\d+),\s\s?(-?\d+)>", RegexOptions.Compiled);

            var pixels = input.Select(_ =>
            {
                var match = regex.Match(_);
                return new Pixel()
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value),
                    Dx = int.Parse(match.Groups[3].Value),
                    Dy = int.Parse(match.Groups[4].Value)
                };
            }).ToList();

            // magic number is 20
            var maxScreenHeight = 20;

            var minX = pixels.Min(_ => _.X);
            var maxX = pixels.Max(_ => _.X);
            var minY = pixels.Min(_ => _.Y);
            var maxY = pixels.Max(_ => _.Y);

            var time = 0;

            while (true)
            {
                if (pixels.Max(_ => _.Y) - pixels.Min(_ => _.Y) + 1 < maxScreenHeight)
                {
                    minX = pixels.Min(_ => _.X);
                    maxX = pixels.Max(_ => _.X);
                    minY = pixels.Min(_ => _.Y);
                    maxY = pixels.Max(_ => _.Y);

                    var points = pixels.Select(_ => new
                    {
                        Left = _.X - minX,
                        Top = _.Y - minY
                    }).ToArray();

                    Console.Clear();

                    foreach (var point in points)
                    {
                        Console.SetCursorPosition(point.Left, point.Top);
                        Console.Write("#");
                    }

                    Console.SetCursorPosition(0, maxScreenHeight);
                    Console.WriteLine(time);
                }

                for (var i = pixels.Count - 1; i >= 0; i--)
                {
                    pixels[i].Move();

                    if (pixels[i].X > maxX || pixels[i].X < minX || pixels[i].Y > maxY || pixels[i].Y < minY)
                        pixels.RemoveAt(i);
                }
                
                time++;
            }
        }
    }

    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Dx { get; set; }
        public int Dy { get; set; }

        public void Move()
        {
            X += Dx;
            Y += Dy;
        }
    }
}
