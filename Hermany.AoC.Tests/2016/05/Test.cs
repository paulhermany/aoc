using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._05
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._05.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("aabc"), "18f47a30");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "2414bc77");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("abc"), "05ace8e3");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "437e60fc");
        }
    }
}
