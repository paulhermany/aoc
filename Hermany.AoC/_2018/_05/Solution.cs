using System;
using System.Collections;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._05
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return React(input[0]).ToString();
        }

        public string Part2(params string[] input)
        {
            var minLength = input[0].Length;

            // remove units one type at a time and fully react the resulting polymer
            foreach (var c in "abcdefghijklmnopqrstuvwxyz")
            {
                var units = React(
                    input[0]
                        .Replace(c.ToString(), string.Empty)
                        .Replace(c.ToString().ToUpper(), string.Empty)
                );

                if (units < minLength)
                    minLength = units;
            }

            return minLength.ToString();
        }

        private static int React(string units)
        {
            var stack = new Stack();
            
            foreach (var u in units)
            {
                if (
                    // can't peek at an empty stack
                    stack.Count > 0
                    // check for reaction (aA or Aa)
                    && (char)stack.Peek() != u
                    && string.Equals(stack.Peek().ToString(), u.ToString(), StringComparison.CurrentCultureIgnoreCase)
                )
                    stack.Pop();
                else
                    stack.Push(u);
            }

            return stack.Count;
        }

    }
}
