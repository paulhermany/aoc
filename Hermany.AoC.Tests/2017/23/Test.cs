using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._23
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._23.Solution();

        [TestMethod]
        [Ignore]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("6724", solution.Part1(input));
        }

        [TestMethod]
        [Ignore]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("903", solution.Part2(input));
        }
    }
}
