using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._01
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._01.Solution();
        
        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("5", solution.Part1("R2, L3"));
            Assert.AreEqual("2", solution.Part1("R2, R2, R2"));
            Assert.AreEqual("12", solution.Part1("R5, L5, R5, R3"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("181", solution.Part1(Program.GetPuzzleInput(solution)));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("4", solution.Part2("R8, R4, R4, R8"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("140", solution.Part2(Program.GetPuzzleInput(solution)));
        }
    }
}
