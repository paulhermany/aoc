﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._14
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._14.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("8108", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("8222", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("1242", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("1086", solution.Part2(input));
        }
    }
}
