using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._10
{
    public class Solution : ISolution
    {
        public int ArraySize { get; set; } = 256;

        public int ChunkSize { get; set; } = 16;
        public string Part1(params string[] input)
        {
            var lengths = input[0].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(_ => int.Parse(_.Trim())).ToArray();

            var array = Enumerable.Range(0, ArraySize).ToArray();

            var index = 0;
            var skip = 0;

            foreach (var length in lengths)
            {               
                for (var i = 0; i < length / 2; i++)
                {
                    var a = (index + i) % array.Length;
                    var b = (index + length - i - 1) % array.Length;

                    var tmp = array[a];
                    array[a] = array[b];
                    array[b] = tmp;
                }

                index += (length + skip++) % array.Length;
            }

            return (array[0] * array[1]).ToString();
        }

        public string Part2(params string[] input)
        {
            var lengths = input[0].Select(Convert.ToInt32).ToList();
            lengths.AddRange("17, 31, 73, 47, 23".Split(',').Select(_ => int.Parse(_.Trim())));
            
            var array = Enumerable.Range(0, ArraySize).ToArray();

            var index = 0;
            var skip = 0;

            for (var r = 0; r < 64; r++)
            {
                foreach (var length in lengths)
                {
                    for (var i = 0; i < length / 2; i++)
                    {
                        var a = (index + i) % array.Length;
                        var b = (index + length - i - 1) % array.Length;

                        var tmp = array[a];
                        array[a] = array[b];
                        array[b] = tmp;
                    }

                    index += (length + skip++) % array.Length;
                }
            }

            var chunks = new List<int>();
            for (var c = 0; c < array.Length; c += ChunkSize)
                chunks.Add(array.Skip(c).Take(ChunkSize).Aggregate((a, b) => a ^ b));

            return string.Concat(chunks.Select(_ => _.ToString("x2")));
        }

    }
}
