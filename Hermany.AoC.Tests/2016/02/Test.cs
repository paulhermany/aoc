using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._02
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._02.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("ULL","RRDDD","LURDL","UUUUD"), "1985");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "53255");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("ULL", "RRDDD", "LURDL", "UUUUD"), "5DB3");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "7423A");
        }
    }
}
