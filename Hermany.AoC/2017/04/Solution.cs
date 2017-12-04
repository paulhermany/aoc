using System.Linq;

namespace Hermany.AoC._2017._04
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input) =>
            input.Select(passphrase => passphrase.Split(' '))
                .Count(words => words.Distinct().Count() == words.Length).ToString();
        
        public string Part2(params string[] input) =>
            input.Select(passphrase => passphrase.Split(' ').Select(word => string.Concat(word.OrderBy(c => c))))
                .Count(words => words.Distinct().Count() == words.Count()).ToString();
    }
}
