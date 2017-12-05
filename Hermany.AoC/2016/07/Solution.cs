using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._07
{
    public class Solution : ISolution
    {
        public const string AbbaPattern = @"(\w)(?!\1)(\w)\2\1";
        public const string HypernetAbbaPattern = @"\[\w*(\w)(?!\1)(\w)\2\1\w*\]";

        public string Part1(params string[] input) =>
            input.Count(_ =>
                Regex.IsMatch(_, AbbaPattern)  &&
                !Regex.IsMatch(_, HypernetAbbaPattern)
            ).ToString();

        public string Part2(params string[] input) =>
            string.Empty;
    }
}
