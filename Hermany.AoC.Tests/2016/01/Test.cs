using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._01
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._01.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("R2, L3"), "5");
            Assert.AreEqual(solution.Part1("R2, R2, R2"), "2");
            Assert.AreEqual(solution.Part1("R5, L5, R5, R3"), "12");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "181");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("R8, R4, R4, R8"), "4");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "140");
        }
    }
}
