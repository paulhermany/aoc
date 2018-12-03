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
        private Dictionary<ValueTuple<int, int>, int> Points = new Dictionary<ValueTuple<int, int>, int>();
        
        public string Part1(params string[] input)
        {
            // parse the things
            var regex =new Regex(@"^#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)$");

            var claims = input.Select(_ =>
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
            });

            foreach (var claim in claims)
            {
                RecordPoints(claim);
            }

            return Points.Count(_ => _.Value > 1).ToString();
        }

        public string Part2(params string[] input)
        {
            Points.Clear();

            var regex = new Regex(@"^#(\d+)\s@\s(\d+),(\d+):\s(\d+)x(\d+)$");

            var claims = input.Select(_ =>
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
            }).ToList();

            foreach (var claim in claims)
            {
                RecordIds(claim);
            }

            return claims.Single(IsSolo).Id.ToString();
        }

        private void RecordPoints(Claim claim)
        {
            for (var x = claim.Left; x < claim.Left + claim.Width; x++)
            {
                for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    var tuple = new ValueTuple<int, int>(x, y);
                    if(!Points.ContainsKey(tuple))
                        Points.Add(tuple, 0);
                    Points[tuple]++;
                }
            }
        }

        private void RecordIds(Claim claim)
        {
            for (var x = claim.Left; x < claim.Left + claim.Width; x++)
            {
                for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    var tuple = new ValueTuple<int, int>(x, y);
                    if (!Points.ContainsKey(tuple))
                        Points.Add(tuple, claim.Id);
                    else
                        Points[tuple] = 0;
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
                    if (!Points.ContainsKey(tuple)) return false;
                    if (Points[tuple] != claim.Id) return false;
                }
            }

            return true;
        }
    }
}
