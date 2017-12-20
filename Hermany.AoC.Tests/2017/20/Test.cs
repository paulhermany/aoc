using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._20
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._20.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.1.txt");
            Assert.AreEqual("0", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("144", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution, "input.sample.2.txt");
            Assert.AreEqual("1", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("477", solution.Part2(input));
        }
    }
}
