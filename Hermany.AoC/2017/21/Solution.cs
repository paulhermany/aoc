using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;

namespace Hermany.AoC._2017._21
{
    public class Solution : ISolution
    {
        public int NumberOfIterations { get; set; } = 2;
        public string Part1(params string[] input) => 
            DoArt(input, NumberOfIterations).Count(_ => _ == '#').ToString();

        public string Part2(params string[] input) => 
            DoArt(input, NumberOfIterations).Count(_ => _ == '#').ToString();
        
        public string DoArt(string[] input, int iterations)
        {
            var rules = input.Select(line =>
            {
                var tokens = line.Split(new[] { " => " }, StringSplitOptions.None);
                return new
                {
                    Input = tokens[0],
                    Output = tokens[1]
                };
            }).ToDictionary(_ => _.Input, _ => _.Output);

            var expandedRules = new Dictionary<string, string>();

            foreach (var rule in rules)
            {
                var rotated = rule.Key;

                for (var i = 0; i < 4; i++)
                {
                    rotated = Rotate(rotated);
                    if (!expandedRules.ContainsKey(rotated))
                        expandedRules.Add(rotated, rule.Value);

                    var flipped = FlipHoriz(rotated);
                    if (!expandedRules.ContainsKey(flipped))
                        expandedRules.Add(flipped, rule.Value);

                    flipped = FlipVert(rotated);
                    if (!expandedRules.ContainsKey(flipped))
                        expandedRules.Add(flipped, rule.Value);
                }
            }

            var current = ".#./..#/###";

            for (var i = 0; i < iterations; i++)
                current = Flatten(Expand(current).Select(_ => expandedRules[_]).ToArray());

            return current;
        }

        public string Rotate(string rhs)
        {
            var arr = rhs.Split('/').Select(_ => _.ToCharArray()).ToArray();

            if (arr.Length == 2)
                return $"{arr[1][0]}{arr[0][0]}/{arr[1][1]}{arr[0][1]}";
            return $"{arr[2][0]}{arr[1][0]}{arr[0][0]}/{arr[2][1]}{arr[1][1]}{arr[0][1]}/{arr[2][2]}{arr[1][2]}{arr[0][2]}";
        }
            

        public string FlipHoriz(string rhs) =>
            string.Join("/", rhs.Split('/').Select(_ => new string(_.ToCharArray()
                .Reverse().ToArray()))
            );

        public string FlipVert(string rhs) =>
            string.Join("/", rhs.Split('/').Reverse().ToArray());
        
        public string[] Expand(string value)
        {
            var dim = (double)value.IndexOf('/');
            var gDim = (int)(dim % 2) == 0 ? 2.0 : 3.0;
            var indexedValues = value
                .Replace("/", string.Empty)
                .Select((c, i) =>
                    {
                        var rIndex = (int) Math.Floor(i / dim);
                        var cIndex = (int) (i % dim);
                        var gRIndex = (int) Math.Floor(rIndex / gDim);
                        var gCIndex = (int) Math.Floor(cIndex / gDim);

                        return new
                        {
                            RowIndex = rIndex,
                            ColIndex = cIndex,
                            ChunkRowIndex = gRIndex,
                            ChunkColIndex = gCIndex,
                            Value = c
                        };
                    }
                );

            return indexedValues
                .GroupBy(_ => _.ChunkRowIndex)
                .SelectMany(rowGrp => rowGrp
                    .GroupBy(_ => _.ChunkColIndex)
                    .Select(colGrp => 
                        string.Join("/",colGrp
                            .GroupBy(_ => _.RowIndex)
                            .Select(chunkRowGroup => 
                                new string(chunkRowGroup.Select(_ => _.Value).ToArray())
                            ).ToArray()
                        )
                    ).ToArray()
                ).ToArray();
        }

        public string Flatten(string[] values)
        {
            if (values.Length == 1) return values[0];

            var dim = (int) Math.Sqrt(values.Length);

            var ret = new List<string>();

            for (var i = 0; i < dim; i++)
                ret.Add(Concatenate(values.Skip(i * dim).Take(dim).ToArray()));

            return string.Join("/", ret.ToArray());
        }

        public string Concatenate(params string[] values)
        {
            var grids = values.Select(_ => _.Split('/').ToArray()).ToArray();

            var sbs = new List<StringBuilder>();
            for(var i = 0; i < grids.First().Length; i++)
                sbs.Add(new StringBuilder());

            foreach (var value in grids)
            {
                for (var i = 0; i < value.Length; i++)
                    sbs[i].Append(value[i]);
            }

            return string.Join("/", sbs.Select(_ => _.ToString()).ToArray());
        }
    }
}
