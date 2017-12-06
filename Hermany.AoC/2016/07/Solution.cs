using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._07
{
    public class Solution : ISolution
    {
        public const string AbbaPattern = @"(\w)(?!\1)(\w)\2\1";
        public const string AbaPattern = @"(\w)(?!\1)(\w)\1";
        public const string HypernetAbbaPattern = @"\[\w*(\w)(?!\1)(\w)\2\1\w*\]";
        public const string HypernetSequencePattern = @"\[\w+\]";

        public string Part1(params string[] input) =>
            input.Count(_ =>
                Regex.IsMatch(_, AbbaPattern) &&
                !Regex.IsMatch(_, HypernetAbbaPattern)
            ).ToString();

        public string Part2(params string[] input) =>
            input.Select(address => new IPv7Address(address))
                .Count(ipv7Address =>
                    ipv7Address.HypernetSequences.Any(_ => AbaToBab(ipv7Address.SupernetSequences).Any(_.Contains)))
                .ToString();

        public static IEnumerable<string> AbaToBab(IEnumerable<string> values)
        {
            var abas = new List<string>();
            foreach (var value in values)
            {
                for (var i = 0; i < value.Length - 2; i++)
                {
                    if(value[i] == value[i + 2])
                        abas.Add($"{value[i+1]}{value[i]}{value[i+1]}");
                }
            }
            return abas;
        }
    }
}
