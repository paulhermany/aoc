using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._21
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._21.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._21.Solution) solution).NumberOfIterations = 2;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("12", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._21.Solution)solution).NumberOfIterations = 5;
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("144", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._21.Solution)solution).NumberOfIterations = 2;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("12", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._21.Solution)solution).NumberOfIterations = 18;
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("2169301", solution.Part2(input));
        }
    }
}
