using System;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._09
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var tokens = input[0].Split(' ');

            return GetMaxScore(int.Parse(tokens[0]), int.Parse(tokens[6])).ToString();
        }

        public string Part2(params string[] input)
        {
            var tokens = input[0].Split(' ');

            return GetMaxScore(int.Parse(tokens[0]), int.Parse(tokens[6]) * 100).ToString();
        }

        private long GetMaxScore(int numberOfPlayers, int lastMarbleValue)
        {

            var ring = new Node(0);
            ring.Next = ring;
            ring.Prev = ring;

            var i = 1;
            var currentNode = ring;
            var currentPlayer = 0;

            var scores = new long[numberOfPlayers];

            while (i < lastMarbleValue)
            {
                if (i % 23 == 0)
                {
                    var removed = currentNode.RemoveAt(-7);

                    currentNode = removed.Next;

                    scores[currentPlayer] += i + removed.Value;
                }
                else
                {
                    currentNode = currentNode.AddAt(i, 1);
                }

                i++;
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            return scores.Max();
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }

        public Node(int value)
        {
            this.Value = value;
        }

        public Node AddAt(int value, int offset)
        {
            var node = new Node(value);

            var a = this;
            for (var i = 0; i < Math.Abs(offset); i ++)
                a = offset < 0 ? a.Prev : a.Next;

            var b = a.Next;

            a.Next = node;
            b.Prev = node;

            node.Next = b;
            node.Prev = a;
            
            return node;
        }

        public Node RemoveAt(int offset)
        {
            var target = this;
            for (var i = 0; i < Math.Abs(offset); i++)
                target = offset < 0 ? target.Prev : target.Next;

            target.Prev.Next = target.Next;
            target.Next.Prev = target.Prev;

            //target.Next = null;
            //target.Prev = null;

            return target;
        }
    }
}
