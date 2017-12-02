using System;
using System.CodeDom;
using Hermany.AoC.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Hermany.AoC.Day02
{
    public class Day02
    {
        public string Part1(string[] input)
        {
            var sum = input.Select(line => line.Split('\t').Select(_ => int.Parse(_.Trim())).ToArray()).Select(arr => arr.Max() - arr.Min()).Sum();
            return sum.ToString();
        }

        public string Part2(string[] input)
        {
            var sum = 0;
            foreach (var line in input)
            {
                var arr = line.Split('\t').Select(_ => int.Parse(_.Trim())).ToArray();

                for (var i = 0; i < arr.Length; i++)
                {
                    for (var j = i + 1; j < arr.Length; j++)
                    {
                        if (arr[i] > arr[j])
                        {
                            if (arr[i] % arr[j] == 0)
                                sum += arr[i] / arr[j];
                        }
                        else
                        {
                            if (arr[j] % arr[i] == 0)
                                sum += arr[j] / arr[i];
                        }
                    }
                }
            }
            return sum.ToString();
        }
    }
}
