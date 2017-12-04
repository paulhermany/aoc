using System;
using System.CodeDom;
using System.Collections.Generic;
using Hermany.AoC.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Hermany.AoC.Day04
{
    public class Day04
    {
        public string Part1(string[] input) =>
            input.Select(passphrase => passphrase.Split(' '))
                .Count(words => words.Distinct().Count() == words.Length).ToString();
        
        public string Part2(string[] input) =>
            input.Select(passphrase => passphrase.Split(' ').Select(word => string.Concat(word.OrderBy(c => c))))
                .Count(words => words.Distinct().Count() == words.Count()).ToString();
    }
}
