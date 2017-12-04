using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2017._01
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input) => 
            input[0].Select(_ => int.Parse(_.ToString())).CaptchaSum().ToString();

        public string Part2(params string[] input) =>
            input[0].Select(_ => int.Parse(_.ToString())).CaptchaSum(input[0].Length / 2).ToString();
    }
}
