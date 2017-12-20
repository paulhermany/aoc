using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hermany.AoC._2017._20
{
    public class Solution : ISolution
    {
        public long Iterations { get; set; } = 1000;

        public string Part1(params string[] input)
        {
            var particles = GetParticles(input).ToArray();

            for(var i = 0; i < Iterations; i++)
                foreach (var particle in particles)
                    particle.Update();

            return particles.OrderBy(_ => _.Distance).First().Index.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var particles = GetParticles(input).ToArray();

            for (var i = 0; i < Iterations; i++)
            {
                var collisions = particles
                    .Where(_ => _.IsLive)
                    .GroupBy(_ => _.P.ToString())
                    .Where(grp => grp.Count() > 1)
                    .SelectMany(grp => grp.Select(_ => _.Index)).ToArray();
                
                foreach (var particle in particles.Where(_ => collisions.Contains(_.Index)))
                    particle.IsLive = false;
                
                foreach (var particle in particles)
                    particle.Update();
            }

            return particles.Count(_ => _.IsLive).ToString();
        }

        public static IEnumerable<Particle> GetParticles(string[] input)
        {
            var particles = input.Select((line, i) =>
            {
                var matches = Regex.Matches(line, @"(\w)=<(-?\d+),(-?\d+),(-?\d+)>").Cast<Match>().ToArray();

                return new Particle()
                {
                    Index = i,
                    P = new Coord3d()
                    {
                        X = long.Parse(matches[0].Groups[2].Value),
                        Y = long.Parse(matches[0].Groups[3].Value),
                        Z = long.Parse(matches[0].Groups[4].Value)
                    },
                    V = new Coord3d()
                    {
                        X = long.Parse(matches[1].Groups[2].Value),
                        Y = long.Parse(matches[1].Groups[3].Value),
                        Z = long.Parse(matches[1].Groups[4].Value)
                    },
                    A = new Coord3d()
                    {
                        X = long.Parse(matches[2].Groups[2].Value),
                        Y = long.Parse(matches[2].Groups[3].Value),
                        Z = long.Parse(matches[2].Groups[4].Value)
                    }
                };
            });

            return particles;
        }
    }
}
