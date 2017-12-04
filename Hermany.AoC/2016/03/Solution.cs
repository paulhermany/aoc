using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._03
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var triangles = input.Select(i => i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(tuple =>
                {
                    var orderedTuple = tuple.Select(i => Convert.ToInt32(i)).OrderByDescending(i => i).ToArray();

                    var thesum = orderedTuple.Skip(1).Take(2).Sum();

                    return thesum > orderedTuple.First();
                }).Count(i => i);

            return triangles.ToString();
        }

        public string Part2(params string[] input)
        {
            var pivot = new List<string>();

            for (var i = 0; i < input.Length; i += 3)
            {
                var tuples = input.Skip(i).Take(3)
                    .Select(
                        line => line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(items => Convert.ToInt32(items))
                    )
                    .Aggregate(new List<int>(), (list, items) =>
                    {
                        list.AddRange(items);
                        return list;
                    })
                    .Select((value, index) => new { value, index })
                    .GroupBy(g => g.index % 3)
                    .Select(h => string.Join(" ", h.Select(j => j.value)));

                pivot.AddRange(tuples);
            }

            return Part1(pivot.ToArray());
        }
    }
}
