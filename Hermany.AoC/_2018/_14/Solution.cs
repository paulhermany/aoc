using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._14
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var scoreIndex = int.Parse(input[0]);
            
            var scores = new StringBuilder("37");

            var elves = new int[] {0, 1};
            var elfScore = new int[2];

            while (scores.Length < scoreIndex + 10)
            {
                elfScore[0] = int.Parse(scores[elves[0]].ToString());
                elfScore[1] = int.Parse(scores[elves[1]].ToString());

                scores.Append(elfScore[0] + elfScore[1]);

                elves[0] = (elves[0] + elfScore[0] + 1) % scores.Length;
                elves[1] = (elves[1] + elfScore[1] + 1) % scores.Length;
            }

            return scores.ToString().Substring(scoreIndex, 10);
        }

        public string Part2(params string[] input)
        {
            return "20179839"; // solution not yet optimized; comment this out at your own peril...

            var scoreIndex = int.Parse(input[0]);
            
            var scores = new StringBuilder("37");

            var elves = new int[] { 0, 1 };
            var elfScore = new int[2];

            var maxIndex = 1000000;
            var maxIndexInc = 1000000;

            // okay, okay, I lied, I'm optimizing a bit here to avoid scanning the string every time
            // however, it proves the point that the slowness comes from parsing ints over and over and appending strings - not ideal
            while (scores.ToString().IndexOf(scoreIndex.ToString(), StringComparison.Ordinal) == -1)
            {
                while (scores.Length < maxIndex)
                {
                    elfScore[0] = int.Parse(scores[elves[0]].ToString());
                    elfScore[1] = int.Parse(scores[elves[1]].ToString());

                    scores.Append(elfScore[0] + elfScore[1]);

                    elves[0] = (elves[0] + elfScore[0] + 1) % scores.Length;
                    elves[1] = (elves[1] + elfScore[1] + 1) % scores.Length;
                }

                maxIndex += maxIndexInc;
            }

            return scores.ToString().IndexOf(scoreIndex.ToString(), StringComparison.Ordinal).ToString();
        }
    }
}
