using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._08
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var width = 25;
            var height = 6;

            return input[0]
                .Select((c, i) => new {c, i})
                .GroupBy(_ => _.i / (width * height))
                .OrderBy(g => g.Count(_ => _.c == '0'))
                .First()
                .GroupBy(_ => _.c)
                .Where(g => new[] {'1', '2'}.Contains(g.Key))
                .Select(g => g.Count())
                .Aggregate((a, b) => a * b).ToString();
        }

        public string Part2(params string[] input)
        {
            var width = 25;
            var height = 6;

            var top = input[0]
                .Select((v, i) =>
                {
                    var layer = i / (width * height);
                    var layerIndex = layer * (width * height);

                    return new {
                        v,
                        Layer = layer,
                        P = ((i - layerIndex) / width, (i - layerIndex) % width)
                    };
                })
                .GroupBy(_ => _.P)
                .Select(g => g.Where(_ => _.v != '2').OrderBy(_ => _.Layer).FirstOrDefault())
                .Where(_ => _ != null)
                .ToDictionary(_ => _.P);

            // print
            var sb = new StringBuilder();

            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c++)
                {
                    if (top.ContainsKey((r, c)) && top[(r,c)].v == '1')
                        sb.Append('#');
                    else
                        sb.Append(' ');
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

    }
}
