using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._09
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var stream = input[0];
            
            var ignored = Regex.Replace(stream, @"\!.", string.Empty);

            var cleaned = Regex.Replace(ignored, @"<[^>]*>", string.Empty);

            var brackets = Regex.Replace(cleaned, @"[^\{\}]", string.Empty);

            Node root = null;
            Node currentNode = null;

            foreach (char b in brackets)
            {
                switch (b)
                {
                    case '}':
                        currentNode = currentNode?.Parent;
                        continue;
                    case '{':
                        if (null == currentNode)
                        {
                            root = new Node();
                            currentNode = root;
                        }
                        else
                        {
                            var node = new Node { Parent = currentNode };
                            currentNode.Children.Add(node);
                            currentNode = node;
                        }
                        continue;
                }
            }

            return root.TotalScore.ToString();
        }

        public string Part2(params string[] input)
        {
            var stream = input[0];

            var ignored = Regex.Replace(stream, @"\!.", string.Empty);

            return Regex.Matches(ignored, @"<[^>]*>").Cast<Match>().Sum(_ => _.Value.Length - 2).ToString();
        }

    }
}
