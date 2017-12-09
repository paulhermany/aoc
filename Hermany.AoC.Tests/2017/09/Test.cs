using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._09
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._09.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1", solution.Part1("{}"));
            Assert.AreEqual("6", solution.Part1("{{{}}}"));
            Assert.AreEqual("5", solution.Part1("{{},{}}"));
            Assert.AreEqual("16", solution.Part1("{{{},{},{{}}}}"));
            Assert.AreEqual("1", solution.Part1("{<a>,<a>,<a>,<a>}"));
            Assert.AreEqual("9", solution.Part1("{{<ab>},{<ab>},{<ab>},{<ab>}}"));
            Assert.AreEqual("9", solution.Part1("{{<!!>},{<!!>},{<!!>},{<!!>}}"));
            Assert.AreEqual("3", solution.Part1("{{<a!>},{<a!>},{<a!>},{<ab>}}"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("16869", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("0", solution.Part2("<>"));
            Assert.AreEqual("17", solution.Part2("<random characters>"));
            Assert.AreEqual("3", solution.Part2("<<<<>"));
            Assert.AreEqual("2", solution.Part2("<{!>}>"));
            Assert.AreEqual("0", solution.Part2("<!!>"));
            Assert.AreEqual("0", solution.Part2("<!!!>>"));
            Assert.AreEqual("10", solution.Part2("<{o\"i!a,<{i<a>"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("7284", solution.Part2(input));
        }
    }
}
