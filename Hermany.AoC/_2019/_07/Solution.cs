using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._07
{
    public class Solution : ISolution
    {

        public string Part1(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            long maxSignal = 0;

            foreach (var phase in GeneratePhase())
            {
                var amps = phase.Select(_ => new Intcode(program, _)).ToArray();

                long inputSignal = 0;

                foreach (var amp in amps)
                {
                    inputSignal = amp.Run(inputSignal).GetValueOrDefault();
                }

                if (inputSignal > maxSignal) maxSignal = inputSignal;
            }

            return maxSignal.ToString();
        }

        public string Part2(params string[] input)
        {
            var program = input[0].Split(',').Select(long.Parse).ToArray();

            long maxSignal = 0;
            
            foreach (var phase in GeneratePhase(5, 10))
            {
                var amps = phase.Select(_ => new Intcode(program, _)).ToArray();

                long inputSignal = 0;

                int currentAmp = 0;

                while (!amps.Last().IsHalted)
                {
                    var amp = amps[currentAmp];

                    amp.NextOutput(inputSignal);
                    inputSignal = amp.Output.Last();
                    
                    currentAmp = (currentAmp + 1) % amps.Length;
                }

                if (inputSignal > maxSignal) maxSignal = inputSignal;
            }

            return maxSignal.ToString();
        }

        private static IEnumerable<int[]> GeneratePhase(int min = 0, int max = 5)
        {
            for (var a = min; a < max; a++)
            {
                for (var b = min; b < max; b++)
                {
                    for (var c = min; c < max; c++)
                    {
                        for (var d = min; d < max; d++)
                        {
                            for(var e = min; e < max; e++)
                            {
                                var arr = new[] {a, b, c, d, e};
                                if(arr.Distinct().Count() == 5)
                                    yield return new []{a,b,c,d,e};
                            }
                        }
                    }
                }
            }
        }
    }
}
