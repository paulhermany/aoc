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

namespace Hermany.AoC._2019._11
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var grid = new Dictionary<(int X, int Y), long>();

            var x = 0;
            var y = 0;
            var t = Math.PI / 2;

            var robot = new Intcode(program);
            
            while (!robot.IsHalted)
            {
                if (!grid.ContainsKey((x, y)))
                    grid.Add((x, y), 0);

                grid[(x, y)] = robot.NextOutput(grid[(x, y)]).GetValueOrDefault();

                if (robot.IsHalted)
                    grid.Remove((x, y));

                var direction = robot.NextOutput().GetValueOrDefault() == 0 ? 1 : -1;

                t += direction * Math.PI / 2;

                x += (int) Math.Cos(t);
                y += (int) Math.Sin(t);
            }
            
            return grid.Count().ToString();
        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var grid = new Dictionary<(int X, int Y), long>();

            grid.Add((0, 0), 1);

            var x = 0;
            var y = 0;
            var t = Math.PI / 2;

            var robot = new Intcode(program);

            while (!robot.IsHalted)
            {

                if (!grid.ContainsKey((x, y)))
                    grid.Add((x, y), 0);
                
                grid[(x, y)] = robot.NextOutput(grid[(x, y)]).GetValueOrDefault();

                var direction = robot.NextOutput().GetValueOrDefault() == 0 ? 1 : -1;

                t += direction * Math.PI / 2;

                x += (int)Math.Cos(t);
                y += (int)Math.Sin(t);
            }

            var sb = new StringBuilder();
            for (var r = grid.Keys.Max(_ => _.Y); r >= grid.Keys.Min(_ => _.Y); r--)
            {
                for (var c = grid.Keys.Min(_ => _.X); c <= grid.Keys.Max(_ => _.X); c++)
                {
                    sb.Append(grid.ContainsKey((c, r)) ? grid[(c, r)] : 0);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
