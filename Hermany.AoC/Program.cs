using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;
using Hermany.AoC._2015._01;

namespace Hermany.AoC
{
    class Program
    {
        private const string InputDirectory = @"Input";
        private const string OutputDirectory = @"Output";

        public static void Main(string[] args)
        {
            var solution = new _2018._01.Solution();

            var date = GetSolutionDate(solution);
            
            var input = System.IO.File.ReadAllLines($@"{InputDirectory}\{date}.txt");

            System.IO.Directory.CreateDirectory(OutputDirectory);

            System.IO.File.WriteAllText($@"{OutputDirectory}\{date}a.txt", solution.Part1(input));
            System.IO.File.WriteAllText($@"{OutputDirectory}\{date}b.txt", solution.Part2(input));
        }

        private static string GetSolutionDate(ISolution solution)
        {
            return string.Join("-12-",
                solution.GetType().FullName?.Split('.').Skip(2).Take(2).Select(_ => _.Replace("_", string.Empty))
                    .ToArray() ?? new string[] { });
        }
    }
}
