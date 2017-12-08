using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._05
{
    [TestClass]
    [Ignore]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._05.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("18f47a30", solution.Part1("abc"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("2414bc77", solution.Part1(Program.GetPuzzleInput(solution)));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("05ace8e3", solution.Part2("abc"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("437e60fc", solution.Part2(Program.GetPuzzleInput(solution)));
        }
    }
}
