using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._16
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._16.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._16.Solution) solution).NumberOfPrograms = 5;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("baedc", solution.Part1(input));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("iabmedjhclofgknp", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._16.Solution)solution).NumberOfPrograms = 5;
            ((AoC._2017._16.Solution) solution).NumberOfIterations = 2;
            var input = Program.GetPuzzleInput(solution, "input.sample.txt");
            Assert.AreEqual("ceadb", solution.Part2(input));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("oildcmfeajhbpngk", solution.Part2(input));
        }
    }
}
