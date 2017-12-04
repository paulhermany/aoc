using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._04
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._04.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1("aaaaa-bbb-z-y-x-123[abxyz]", "a-b-c-d-e-f-g-h-987[abcde]", "not-a-real-room-404[oarel]", "totally-real-room-200[decoy]"), "1514");
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part1(Program.GetPuzzleInput(solution)), "361724");
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual(solution.Part2(Program.GetPuzzleInput(solution)), "482");
        }
    }
}
