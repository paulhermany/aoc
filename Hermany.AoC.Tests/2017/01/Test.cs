using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._01
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._01.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("1122"), "3");
            Assert.AreEqual(solution.Part1("1111"), "4");
            Assert.AreEqual(solution.Part1("1234"), "0");
            Assert.AreEqual(solution.Part1("91212129"), "9");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "1029");
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2("1212"), "6");
            Assert.AreEqual(solution.Part2("1221"), "0");
            Assert.AreEqual(solution.Part2("123425"), "4");
            Assert.AreEqual(solution.Part2("123123"), "12");
            Assert.AreEqual(solution.Part2("12131415"), "4");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "1220");
        }
    }
}
