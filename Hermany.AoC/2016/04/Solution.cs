using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2016._04
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var total = 0;

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"((?:(?:\w+)-)+)(\d+)\[(\w{5})\]");

                var text = match.Groups[1].Value;
                var sectorId = match.Groups[2].Value;
                var checksum = match.Groups[3].Value;

                var generatedChecksum = GenerateChecksum(text);

                if (checksum == generatedChecksum) total += Convert.ToInt32(sectorId);
            }

            return total.ToString();
        }

        public string Part2(params string[] input)
        {
            var sb = new StringBuilder();

            foreach (var line in input)
            {
                var match = Regex.Match(line, @"((?:(?:\w+)-)+)(\d+)\[(\w{5})\]");

                var text = match.Groups[1].Value;
                var sectorId = match.Groups[2].Value;
                var checksum = match.Groups[3].Value;

                var generatedChecksum = GenerateChecksum(text);

                if (checksum != generatedChecksum) continue;

                if (Decrypt(text, Convert.ToInt32(sectorId)).Trim() == "northpole object storage")
                    return sectorId;
            }

            return string.Empty;
        }

        public static string GenerateChecksum(string value)
        {
            return value.Where(c => c != '-')
                .GroupBy(c => c)
                .ToDictionary(grp => grp.Key, grp => grp.Count())
                .OrderByDescending(grp => grp.Value)
                .ThenBy(grp => grp.Key)
                .Take(5)
                .Select(grp => grp.Key)
                .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString();
        }

        public static string Decrypt(string value, int key)
        {
            return value
                .Select(c =>
                {
                    if (c == '-') return ' ';
                    return (char) (((int) c - (int) 'a' + key) % 26 + (int) 'a');
                })
                .Aggregate(new StringBuilder(), (sb, c) => sb.Append(c)).ToString();
        }
    }
}
