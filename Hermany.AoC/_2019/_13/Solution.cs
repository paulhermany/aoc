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
using System.Text.RegularExpressions;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._13
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var grid = new Dictionary<(long X, long Y), long>();

            var robot = new Intcode(program);

            while (!robot.IsHalted)
            {
                var x = robot.NextOutput().GetValueOrDefault();
                var y = robot.NextOutput().GetValueOrDefault();
                var t = robot.NextOutput().GetValueOrDefault();

                grid[(x, y)] = t;
            }

            var blocks = grid.Values.Count(_ => _ == 2);

            return blocks.ToString();
        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            var grid = new Dictionary<(long X, long Y), long>();

            long x = 0;
            long y = 0;
            long t = 0;

            long score = 0;

            var game = new Intcode(program);
            game.Hack(0, 2);

            while (!game.IsHalted)
            {
                if (game.IsWaitingInput)
                {
                    //Console.Clear();
                    //for (var r = 0; r < grid.Keys.Max(_ => _.Y); r++)
                    //{
                    //    for (var c = 0; c < grid.Keys.Max(_ => _.X); c++)
                    //    {
                    //        var pt = (c, r);
                    //        if (grid.ContainsKey(pt))
                    //            Console.Write(grid[pt]);
                    //        else
                    //            Console.Write(" ");
                    //    }

                    //    Console.WriteLine();
                    //}

                    //Console.WriteLine();
                    //Console.WriteLine(score);
                    //Console.ReadKey();

                    var i = 0;

                    // extra ball at (4,4) needs to be hacked out
                    // replacing it with a block does nothing since it never reads input back from the grid
                    if (grid.Count(_ => _.Value == 4) > 1)
                    {
                        // luckily the (4,4) position is far enough from the paddle not to cause a problem if the actual ball is there
                        // maybe moving the joystick doesn't matter?
                        if (grid[(4, 4)] == 4)
                            grid[(4, 4)] = 2;
                    }
                    
                    var ball = grid.Single(_ => _.Value == 4);
                    var paddle = grid.Single(_ => _.Value == 3);
                    if (ball.Key.X < paddle.Key.X) i = -1;
                    if (ball.Key.X > paddle.Key.X) i = 1;
                    
                    game.AddInput(i);
                }

                x = game.NextOutput().GetValueOrDefault();
                y = game.NextOutput().GetValueOrDefault();
                t = game.NextOutput().GetValueOrDefault();

                if (x == -1 && y == 0) score = t;

                grid[(x, y)] = t;
            }
            
            return score.ToString();
        }
    }
}
