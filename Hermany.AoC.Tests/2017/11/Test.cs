using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._11
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._11.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("3", solution.Part1("ne,ne,ne"));
            Assert.AreEqual("0", solution.Part1("ne,ne,sw,sw"));
            Assert.AreEqual("2", solution.Part1("ne,ne,s,s"));
            Assert.AreEqual("3", solution.Part1("se,sw,se,sw,sw"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("824", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("2", solution.Part2("ne,ne,sw,sw"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("1548", solution.Part2(input));
        }
    }
}
