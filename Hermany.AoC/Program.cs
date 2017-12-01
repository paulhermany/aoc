using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathToExe = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            
            var input = File.ReadAllLines(Path.Combine(pathToExe, "Day01/input.txt"));
            var day01 = new Day01.Day01();

            var part1 = day01.Part1(input);
            File.WriteAllText(Path.Combine(pathToExe, "Day01/output-part1.txt"), part1);
            Console.WriteLine(part1);

            var part2 = day01.Part2(input);
            File.WriteAllText(Path.Combine(pathToExe, "Day01/output-part2.txt"), part2);
            Console.WriteLine(part2);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
