using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hermany.AoC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var day = new Day02.Day02();

            var dayName = day.GetType().Name;

            var pathToExe = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            var input = File.ReadAllLines(Path.Combine(pathToExe, $"{dayName}/input.txt"));
            
            var part1 = day.Part1(input);
            File.WriteAllText(Path.Combine(pathToExe, $"{dayName}/output-part1.txt"), part1);
            Console.WriteLine(part1);

            var part2 = day.Part2(input);
            File.WriteAllText(Path.Combine(pathToExe, $"{dayName}/output-part2.txt"), part2);
            Console.WriteLine(part2);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        
    }
}
