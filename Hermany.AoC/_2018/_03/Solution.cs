using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._03
{
    public class Solution : ISolution
    {
        // storing a dictionary of x,y coords and either the claim count per "square" or the id of the claim for part 2
        private readonly Dictionary<ValueTuple<int, int>, List<int>> _points = new Dictionary<ValueTuple<int, int>, List<int>>();
        
        public string Part1(params string[] input)
        {
            var claims = ParseClaims(input);

            _points.Clear();
            foreach (var claim in claims)
                RecordIds(claim);

            return _points.Count(_ => _.Value.Count > 1).ToString();
        }

        public string Part2(params string[] input)
        {
            var claims = ParseClaims(input);

            _points.Clear();
            foreach (var claim in claims)
                RecordIds(claim);

            return claims.Single(IsSolo).Id.ToString();
        }

        private Claim[] ParseClaims(string[] input)
        {
            var regex = new Regex(@"^#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)$");

            return input.Select(_ =>
            {
                var match = regex.Match(_);
                return new Claim()
                {
                    Id = int.Parse(match.Groups[1].Value),
                    Left = int.Parse(match.Groups[2].Value),
                    Top = int.Parse(match.Groups[3].Value),
                    Width = int.Parse(match.Groups[4].Value),
                    Height = int.Parse(match.Groups[5].Value)
                };
            }).ToArray();
        }

        private void RecordIds(Claim claim)
        {
            for (var x = claim.Left; x < claim.Left + claim.Width; x++)
            {
                for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    var tuple = new ValueTuple<int, int>(x, y);
                    if (!_points.ContainsKey(tuple))
                        _points.Add(tuple, new List<int>());
                    
                    _points[tuple].Add(claim.Id);
                }
            }
        }

        private bool IsSolo(Claim claim)
        {
            for (var x = claim.Left; x < claim.Left + claim.Width; x++)
            {
                for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    var tuple = new ValueTuple<int, int>(x, y);
                    if (!_points.ContainsKey(tuple) || _points[tuple].Count > 1) return false;
                }
            }

            return true;
        }
    }
}
