using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._06
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._06.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("eedadn", "drvtee", "eandsr", "raavrd", "atevrs", "tsrnev", "sdttsa", "rasrtv", "nssdts", "ntnada", "svetve", "tesnvt", "vntsnd", "vrdear", "dvrsen", "enarar"), "easter");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "liwvqppc");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("eedadn", "drvtee", "eandsr", "raavrd", "atevrs", "tsrnev", "sdttsa", "rasrtv", "nssdts", "ntnada", "svetve", "tesnvt", "vntsnd", "vrdear", "dvrsen", "enarar"), "advent");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "caqfbzlh");
        }
    }
}
