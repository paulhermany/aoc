using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests._2016._07
{
    [TestClass]
    public class Test
    {
        public ISolution CreateSolution() =>
            new AoC._2016._07.Solution();

        [TestMethod]
        public void Part1SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1", solution.Part1("abba[mnop]qrst"));
            Assert.AreEqual("0", solution.Part1("abcd[bddb]xyyx"));
            Assert.AreEqual("0", solution.Part1("aaaa[qwer]tyui"));
            Assert.AreEqual("1", solution.Part1("ioxxoj[asdfgh]zxcvbn"));
        }

        [TestMethod]
        public void Part1PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("118", solution.Part1(Program.GetPuzzleInput(solution)));
        }

        [TestMethod]
        public void Part2SampleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("1", solution.Part2("aba[bab]xyz"));
            Assert.AreEqual("0", solution.Part2("xyx[xyx]xyx"));
            Assert.AreEqual("1", solution.Part2("aaa[kek]eke"));
            Assert.AreEqual("1", solution.Part2("zazbz[bzb]cdb"));
        }

        [TestMethod]
        public void Part2PuzzleInput()
        {
            var solution = CreateSolution();
            Assert.AreEqual("260", solution.Part2(Program.GetPuzzleInput(solution)));
        }
    }
}
