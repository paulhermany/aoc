using System;
using Hermany.AoC.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hermany.AoC.Tests.Common
{
    [TestClass]
    public class LinqExtensionsTest
    {
        [TestMethod]
        public void Pivot()
        {
            var input = new[] {"abc", "def", "ghi"};
            var output = new[] {"adg", "beh", "cfi"};
            Assert.AreEqual(string.Join(string.Empty, output), string.Join(string.Empty, input.Pivot()));
        }
    }
}
