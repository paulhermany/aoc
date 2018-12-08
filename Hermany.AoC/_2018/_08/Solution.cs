using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._08
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return GenerateTree(input)
                .MetadataSum().ToString();
        }

        public string Part2(params string[] input)
        {
            return GenerateTree(input)
                .MetadataValue().ToString();
        }

        public Node GenerateTree(string[] input)
        {
            // convert the list of ints to a queue
            var arr = input[0].Split(' ').Select(int.Parse).ToArray();
            var queue = new Queue<int>(arr);

            // parse the root node and call the recursive parse method to generate the rest of the tree
            var root = new Node(queue.Dequeue(), queue.Dequeue());
            Parse(root, queue);

            return root;
        }

        public void Parse(Node node, Queue<int> queue)
        {
            // loop through the child nodes grabbing the header data from the start of the queue
            for (var i = 0; i < node.QuantityOfChildNodes; i++)
            {
                // create the new node and add it to the current node, then call the recursive parse method
                var child = new Node(queue.Dequeue(), queue.Dequeue());
                node.ChildNodes.Add(child);
                Parse(child, queue);
            }

            // loop through the metadata entries and add them to the node
            for (var i = 0; i < node.QuantityOfMetadataEntries; i++)
            {
                node.MetadataEntries.Add(queue.Dequeue());
            }
        }

    }

    public class Node
    {
        public int QuantityOfChildNodes { get; set; }
        public int QuantityOfMetadataEntries { get; set; }
        public List<Node> ChildNodes { get; }
        public List<int> MetadataEntries { get; }
        
        public Node(int quantityOfChildNodes, int quantityOfMetadataEntries)
        {
            QuantityOfChildNodes = quantityOfChildNodes;
            QuantityOfMetadataEntries = quantityOfMetadataEntries;
            ChildNodes = new List<Node>();
            MetadataEntries = new List<int>();
        }

        // simple tree sum - sum of metadata + sum of child metadata (recursive)
        public int MetadataSum()
        {
            return MetadataEntries.Sum() + ChildNodes.Sum(_ => _.MetadataSum());
        }

        // not so simple sum - sum of metadata if no children and sum of children indexed by metadata
        public int MetadataValue()
        {
            if (ChildNodes.Count == 0)
                return MetadataEntries.Sum();

            var value = 0;
            foreach (var index in MetadataEntries)
            {
                if (index <= ChildNodes.Count)
                    value += ChildNodes[index - 1].MetadataValue();
            }

            return value;
        }
    }

}
