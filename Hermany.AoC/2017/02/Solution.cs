using System.Linq;

namespace Hermany.AoC._2017._02
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var sum = input.Select(line => line.Split('\t').Select(_ => int.Parse(_.Trim())).ToArray()).Select(arr => arr.Max() - arr.Min()).Sum();
            return sum.ToString();
        }

        public string Part2(params string[] input)
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
