using Hermany.AoC.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hermany.AoC
{
    public class Program
    {
        private const string DefaultDate = @"2018-12-01";
        private const string DefaultInputDirectory = @"..\..\..\Hermany.Aoc.Files\Input";
        private const string DefaultOutputDirectory = @"..\..\..\Hermany.Aoc.Files\Output";

        public static void Main(string[] args)
        {
            var date = DateTime.Parse((args.Length > 0 ? args[0] : null) ?? DefaultDate);
            var inputDirectory = (args.Length > 1 ? args[1] : null) ?? DefaultInputDirectory;
            var outputDirectory = (args.Length > 2 ? args[2] : null) ?? DefaultOutputDirectory;

            if (!Directory.Exists(inputDirectory))
                throw new ArgumentException("The specified input directory does not exist.");
            
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory($@"{outputDirectory}\{date:yyyy}");

            var ns = typeof(Program).Namespace;
            var solution = (ISolution)Activator.CreateInstance(ns, $"{ns}._{date:yyyy}._{date:dd}.Solution").Unwrap();
            
            var input = File.ReadAllLines($@"{inputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}.txt");

            var sw = new Stopwatch();

            sw.Start();
            var part1 = solution.Part1(input);
            sw.Stop();
            var part1Elapsed = sw.Elapsed;

            sw.Restart();
            var part2 = solution.Part2(input);
            sw.Stop();
            var part2Elapsed = sw.Elapsed;

            File.WriteAllText($@"{outputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}a.txt", part1);
            File.WriteAllText($@"{outputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}b.txt", part2);

            var pathToSummaryFile = $@"{outputDirectory}\Summary.txt";            
            var summary = File.ReadAllLines(pathToSummaryFile).ToList();
            var summaryIndex = $"{date:yyyy-MM-dd}";

            summary.RemoveAll(_ => _.StartsWith(summaryIndex));
            summary.Add($"{summaryIndex}a ({part1Elapsed:g}): {part1}");
            summary.Add($"{summaryIndex}b ({part2Elapsed:g}): {part2}");
            summary.Sort();

            File.WriteAllLines(pathToSummaryFile, summary);
        }
    }
}
