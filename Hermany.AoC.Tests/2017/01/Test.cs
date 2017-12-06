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
            Assert.AreEqual("3", solution.Part1("1122"));
            Assert.AreEqual("4", solution.Part1("1111"));
            Assert.AreEqual("0", solution.Part1("1234"));
            Assert.AreEqual("9", solution.Part1("91212129"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("1029", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("6", solution.Part2("1212"));
            Assert.AreEqual("0", solution.Part2("1221"));
            Assert.AreEqual("4", solution.Part2("123425"));
            Assert.AreEqual("12", solution.Part2("123123"));
            Assert.AreEqual("4", solution.Part2("12131415"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("1220", solution.Part2(input));
        }
    }
}
