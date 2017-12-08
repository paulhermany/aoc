using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._04
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._04.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1", solution.Part1("aa bb cc dd ee"));
            Assert.AreEqual("0", solution.Part1("aa bb cc dd aa"));
            Assert.AreEqual("1", solution.Part1("aa bb cc dd aaa"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("325", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1", solution.Part2("abcde fghij"));
            Assert.AreEqual("0", solution.Part2("abcde xyz ecdab"));
            Assert.AreEqual("1", solution.Part2("a ab abc abd abf abj"));
            Assert.AreEqual("1", solution.Part2("iiii oiii ooii oooi oooo"));
            Assert.AreEqual("0", solution.Part2("oiii ioii iioi iiio"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("119", solution.Part2(input));
        }
    }
}
