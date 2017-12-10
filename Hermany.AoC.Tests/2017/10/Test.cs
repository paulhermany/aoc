using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2017._10
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2017._10.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            ((AoC._2017._10.Solution) solution).ArraySize = 5;
            Assert.AreEqual("12", solution.Part1("3,4,1,5"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("19591", solution.Part1(input));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", solution.Part2(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", solution.Part2("AoC 2017"));
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", solution.Part2("1,2,3"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", solution.Part2("1,2,4"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            var input = Program.GetPuzzleInput(solution);
            Assert.AreEqual("62e2204d2ca4f4924f6e7a80f1288786", solution.Part2(input));
        }
    }
}
