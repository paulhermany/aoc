using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._03
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._03.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("0", solution.Part1("    5   10   25"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("982", solution.Part1(Program.GetPuzzleInput(solution)));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("6", solution.Part2("101 301 501", "102 302 502", "103 303 503", "201 401 601", "202 402 602", "203 403 603"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1826", solution.Part2(Program.GetPuzzleInput(solution)));
        }
    }
}