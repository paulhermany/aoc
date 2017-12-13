using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._13
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._13.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("24", solution.Part1(
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("1840", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("10", solution.Part2(
                "0: 3",
                "1: 2",
                "4: 4",
                "6: 4"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("3850260", solution.Part2(input));
        }
    }
}
