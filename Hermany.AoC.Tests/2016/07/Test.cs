using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._07
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._07.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("abba[mnop]qrst"), "1");
            Assert.AreEqual(solution.Part1("abcd[bddb]xyyx"), "0");
            Assert.AreEqual(solution.Part1("aaaa[qwer]tyui"), "0");
            Assert.AreEqual(solution.Part1("ioxxoj[asdfgh]zxcvbn"), "1");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "118");
        }

        [TestMethod]
        [Ignore]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("aba[bab]xyz"), "1");
            Assert.AreEqual(solution.Part1("xyx[xyx]xyx"), "0");
            Assert.AreEqual(solution.Part1("aaa[kek]eke"), "1");
            Assert.AreEqual(solution.Part1("zazbz[bzb]cdb"), "1");
        }

        [TestMethod]
        [Ignore]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "260");
        }
    }
}
