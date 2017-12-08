using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2017._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input) => 
            CaptchaSum(input[0].Select(_ => int.Parse(_.ToString()))).ToString();

        public string Part2(params string[] input) =>
            CaptchaSum(input[0].Select(_ => int.Parse(_.ToString())),input[0].Length / 2).ToString();

        public static int CaptchaSum(IEnumerable<int> values, int compareIndexOffset = 1)
        {
            var arr = values.ToArray();
            return arr.Where((t, i) => t == arr[(i + compareIndexOffset) % arr.Length]).Sum();
        }
    }
}
