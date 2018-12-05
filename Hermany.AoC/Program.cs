using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;
using Hermany.AoC._2015._01;

namespace Hermany.AoC
{
    public class Program
    {
        private const string DefaultDate = @"2018-12-01";
        private const string DefaultInputDirectory = @"..\..\..\Hermany.Aoc.Files\Input";
        private const string DefaultOutputDirectory = @"..\..\..\Hermany.Aoc.Files\Output";

        public static void Main(string[] args)
        {
            var date = DateTime.Parse(args.Length > 0 ? args[0] : null ?? DefaultDate);
            var inputDirectory = (args.Length > 1 ? args[0] : null) ?? DefaultInputDirectory;
            var outputDirectory = (args.Length > 2 ? args[0] : null) ?? DefaultOutputDirectory;

            if (!System.IO.Directory.Exists(inputDirectory))
                throw new ArgumentException("The specified input directory does not exist.");
            
            if (!System.IO.Directory.Exists(outputDirectory))
                throw new ArgumentException("The specified output directory does not exist.");

            var ns = typeof(Program).Namespace;

            var solution = (ISolution)Activator.CreateInstance(ns, $"{ns}._{date:yyyy}._{date:dd}.Solution").Unwrap();
            
            var input = System.IO.File.ReadAllLines($@"{inputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}.txt");

            System.IO.Directory.CreateDirectory($@"{outputDirectory}\{date:yyyy}");

            System.IO.File.WriteAllText($@"{outputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}a.txt", solution.Part1(input));
            System.IO.File.WriteAllText($@"{outputDirectory}\{date:yyyy}\{date:yyyy-MM-dd}b.txt", solution.Part2(input));
        }
    }
}
