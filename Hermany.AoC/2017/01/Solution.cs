using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2017._01
{
    /// <summary>Inverse Captcha
    /// https://adventofcode.com/2017/day/1
    /// </summary>
    public class Solution : ISolution
    {
        public string Part1(params string[] input) => 
            CaptchaSum(ConvertToInt32(input[0])).ToString();

        public string Part2(params string[] input) =>
            CaptchaSum(ConvertToInt32(input[0]),input[0].Length / 2).ToString();

        public static IEnumerable<int> ConvertToInt32(string value) =>
            value.Select(_ => _ - 48);

        public static int CaptchaSum(IEnumerable<int> values, int compareIndexOffset = 1)
        {
            var arr = values.ToArray();
            return arr.Where((t, i) => t == arr[(i + compareIndexOffset) % arr.Length]).Sum();
        }
    }
}
