using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._04
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._04.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("aa bb cc dd ee"), "1");
            Assert.AreEqual(solution.Part1("aa bb cc dd aa"), "0");
            Assert.AreEqual(solution.Part1("aa bb cc dd aaa"), "1");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual(solution.Part1(input), "325");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("abcde fghij"), "1");
            Assert.AreEqual(solution.Part2("abcde xyz ecdab"), "0");
            Assert.AreEqual(solution.Part2("a ab abc abd abf abj"), "1");
            Assert.AreEqual(solution.Part2("iiii oiii ooii oooi oooo"), "1");
            Assert.AreEqual(solution.Part2("oiii ioii iioi iiio"), "0");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual(solution.Part2(input), "119");
        }
    }
}
