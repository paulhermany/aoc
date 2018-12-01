using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;

namespace Hermany.AoC._2015._02
{
    public class Solution : ISolution
    {
        public string[] Part1(params string[] input)
        {
            var boxes = input
                .Select(_ => _.Split('x').Select(int.Parse).ToArray())
                .Select(_ => new {L = _[0], W = _[1], H = _[2]});

            var surfaceArea = boxes
                .Select(_ => new[] {_.L * _.W, _.W * _.H, _.H * _.L})
                .Select(_ => _.Aggregate(0, (a, b) => a + 2 * b) + _.Min())
                .Sum();

            return new[] {surfaceArea.ToString()};
        }

        public string[] Part2(params string[] input)
        {
            var boxes = input
                .Select(_ => _.Split('x').Select(int.Parse).ToArray())
                .Select(_ => new { L = _[0], W = _[1], H = _[2] });

            var length = boxes
                .Select(_ => _.L * _.W * _.H + new[] { 2 * _.L + 2 * _.W, 2 * _.W + 2 * _.H, 2 * _.H + 2 * _.L }.Min())
                .Sum();

            return new[] { length.ToString() };
        }
        
    }
}
