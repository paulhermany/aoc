using System;
using System.IO;
using System.Linq;

namespace Hermany.AoC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var solution = new _2017._11.Solution();

            var path = GetPath(solution);

            var input = GetPuzzleInput(path);
            
            var part1 = solution.Part1(input);
            File.WriteAllText(Path.Combine(path, "output-part1.txt"), part1);
            Console.WriteLine(part1);

            var part2 = solution.Part2(input);
            File.WriteAllText(Path.Combine(path, "output-part2.txt"), part2);
            Console.WriteLine(part2);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        
        public static string[] GetPuzzleInput(ISolution solution)
        {
            return GetPuzzleInput(GetPath(solution));
        }

        private static string[] GetPuzzleInput(string path)
        {
            return File.ReadAllLines(Path.Combine(path, "input.txt"));
        }

        private static string GetPath(ISolution solution)
        {
            var pathToExe = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var path = Path.Combine((solution.GetType().Namespace ?? string.Empty).Split('.').Skip(2).Select(_ => _.Substring(1)).ToArray());
            return Path.Combine(pathToExe, path);
        }
    }
}
