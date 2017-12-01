using Hermany.AoC.Common;
using System.Linq;

namespace Hermany.AoC.Day01
{
    public class Day01
    {
        public string Part1(string[] input) => 
            input[0].Select(_ => int.Parse(_.ToString())).CaptchaSum().ToString();

        public string Part2(string[] input) =>
            input[0].Select(_ => int.Parse(_.ToString())).CaptchaSum(input[0].Length / 2).ToString();
    }
}
