using System;
using System.Linq;
using System.Text;
using Hermany.AoC.Common;

namespace Hermany.AoC._2016._02
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var keypad = Keypad.GetPart1Keypad();

            var pressed = new StringBuilder();

            var currentNode = keypad['5'];

            foreach (var instruction in input)
            {
                var steps = instruction.ToCharArray();

                foreach (var step in steps)
                    if (currentNode.Nodes[step] != null) currentNode = currentNode.Nodes[step];

                pressed.Append(currentNode.Value);
            }

            return pressed.ToString();
        }

        public string Part2(params string[] input)
        {
            var keypad = Keypad.GetPart2Keypad();

            var pressed = new StringBuilder();

            var currentNode = keypad['5'];

            foreach (var instruction in input)
            {
                var steps = instruction.ToCharArray();

                foreach (var step in steps)
                    if (currentNode.Nodes[step] != null) currentNode = currentNode.Nodes[step];

                pressed.Append(currentNode.Value);
            }

            return pressed.ToString();
        }
    }
}
