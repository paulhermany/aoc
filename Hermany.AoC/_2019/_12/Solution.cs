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

namespace Hermany.AoC._2019._12
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var moons = input.Select(Moon.Parse).ToArray();

            var steps = 1000;

            for (var i = 0; i < steps; i++)
            {
                foreach (var moon in moons)
                    moon.ApplyGravity(moons.Except(new[] {moon}).ToArray());

                foreach (var moon in moons)
                    moon.Step();
            }

            var totalEnergy = moons.Sum(_ => _.TotalEnergy);

            return totalEnergy.ToString();
        }

        public string Part2(params string[] input)
        {
            var moons = input.Select(Moon.Parse).ToArray();

            var states = new { X = new HashSet<string>(), Y = new HashSet<string>(), Z = new HashSet<string>()};
            var cycleSteps = Vector3.Zero();
            
            int steps = 0;

            do
            {
                foreach (var moon in moons)
                    moon.ApplyGravity(moons.Except(new[] {moon}).ToArray());

                foreach (var moon in moons)
                    moon.Step();

                steps++;

                string hash = string.Empty;

                if (cycleSteps.X == 0)
                {
                    hash = string.Concat(string.Join(",", moons.Select(_ => _.P.X)), ";",
                        string.Join(",", moons.Select(_ => _.V.X)));

                    if (states.X.Contains(hash))
                        cycleSteps.X = steps;
                    else
                        states.X.Add(hash);
                }

                if (cycleSteps.Y == 0)
                {
                    hash = string.Concat(string.Join(",", moons.Select(_ => _.P.Y)), ";",
                        string.Join(",", moons.Select(_ => _.V.Y)));

                    if (states.Y.Contains(hash))
                        cycleSteps.Y = steps;
                    else
                        states.Y.Add(hash);
                }

                if (cycleSteps.Z == 0)
                {
                    hash = string.Concat(string.Join(",", moons.Select(_ => _.P.Z)), ";",
                        string.Join(",", moons.Select(_ => _.V.Z)));

                    if (states.Z.Contains(hash))
                        cycleSteps.Z = steps;
                    else
                        states.Z.Add(hash);
                }

            } while (cycleSteps.X == 0 || cycleSteps.Y == 0 || cycleSteps.Z == 0);
            
            return $"LCM({cycleSteps.X - 1},{cycleSteps.Y - 1},{cycleSteps.Z - 1})";
        }


    }

    public class Moon
    {

        public Vector3 P { get; set; }
        public Vector3 V { get; set; }


        public bool IsInitialPosition { get; private set; }
        public bool IsStopped { get; private set; }
        public bool IsInitialState => IsInitialPosition && IsStopped;
        

        public int PotentialEnergy => Math.Abs(P.X) + Math.Abs(P.Y) + Math.Abs(P.Z);

        public int KineticEnergy => Math.Abs(V.X) + Math.Abs(V.Y) + Math.Abs(V.Z);

        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        public static Moon Parse(string input)
        {
            return new Moon
            {

                P = Vector3.Parse(input),
                V = Vector3.Zero(),
                InitialPosition = Vector3.Parse(input)
            };
        }

        public void ApplyGravity(params Moon[] moons)
        {
            V.X += moons.Select(_ => _.P.X).Sum(_ => _ == P.X ? 0 : _ > P.X ? 1 : -1);
            V.Y += moons.Select(_ => _.P.Y).Sum(_ => _ == P.Y ? 0 : _ > P.Y ? 1 : -1);
            V.Z += moons.Select(_ => _.P.Z).Sum(_ => _ == P.Z ? 0 : _ > P.Z ? 1 : -1);
        }

        public void Step()
        {
            P.X += V.X;
            P.Y += V.Y;
            P.Z += V.Z;

            IsInitialPosition = P.X == InitialPosition.X && P.Y == InitialPosition.Y && P.Z == InitialPosition.Z;
            
            IsStopped = V.X == 0 && V.Y == 0 && V.Z == 0;
        }

        public override string ToString()
        {
            return $"{ToPositionString()}, {ToVelocityString()}";
        }

        public string ToPositionString()
        {
            return $"pos=<x={P.X}, y={P.Y}, z={P.Z}>";
        }

        public string ToVelocityString()
        {
            return $"vel=<x={V.X}, y={V.Y}, z={V.Z}>";
        }

        private Vector3 InitialPosition { get; set; }

    }

    public class Vector3
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public static Vector3 Parse(string input)
        {
            var match = Pattern.Match(input);
            return new Vector3
            {

                X = int.Parse(match.Groups[1].Value),
                Y = int.Parse(match.Groups[2].Value),
                Z = int.Parse(match.Groups[3].Value)
            };
        }

        public static Vector3 Zero()
        {
            return new Vector3 {X = 0, Y = 0, Z = 0};
        }

        private static readonly Regex Pattern = new Regex(@"^<x=(-?\d+),\sy=(-?\d+),\sz=(-?\d+)>$", RegexOptions.Compiled);
    }
}
