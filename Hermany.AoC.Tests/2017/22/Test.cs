using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._22
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._22.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._22.Solution) solution).NumberOfIterations = 10000;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("5587", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._22.Solution)solution).NumberOfIterations = 10000;
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("5322", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._22.Solution)solution).NumberOfIterations = 10000000;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("2511944", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._22.Solution)solution).NumberOfIterations = 10000000;
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("2512079", solution.Part2(input));
        }
    }
}
