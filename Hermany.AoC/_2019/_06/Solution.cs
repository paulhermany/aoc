using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2019._06
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var nodes = input.Select(Node.ParseNode).ToDictionary(_ => _.Name);

            nodes.Add("COM", new Node {Name = "COM"});

            foreach (var node in nodes.Values)
            {
                if(!string.IsNullOrEmpty(node.ParentName))
                    node.Parent = nodes[node.ParentName];
            }

            var depth = 0;

            foreach (var node in nodes.Values)
                depth += node.GetDepth() - 1;

            return depth.ToString();
        }

        public string Part2(params string[] input)
        {
            var nodes = input.Select(Node.ParseNode).ToDictionary(_ => _.Name);

            nodes.Add("COM", new Node { Name = "COM" });

            foreach (var node in nodes.Values)
            {
                if (!string.IsNullOrEmpty(node.ParentName))
                    node.Parent = nodes[node.ParentName];
            }

            var transfers = 0;

            var san = nodes["SAN"].Parent;
            
            var current = san;

            while (null != current)
            {
                current.DistanceFromSAN = transfers++;
                current = current.Parent;
            }

            var you = nodes["YOU"].Parent;
            current = you;

            transfers = 0;
            while (current.DistanceFromSAN == null)
            {
                transfers++;
                current = current.Parent;
            }

            transfers += current.DistanceFromSAN.Value;

            return transfers.ToString();
        }

        public class Node
        {
            public string Name { get; set; }
            public string ParentName { get; set; }
            public Node Parent { get; set; }
            public int? DistanceFromSAN { get; set; }

            public int GetDepth()
            {
                if (null == Parent)
                    return 1;
                return 1 + Parent.GetDepth();
            }

            public static Node ParseNode(string map)
            {
                var tokens = map.Split(')');
                return new Node()
                {
                    Name = tokens[1],
                    ParentName = tokens[0]
                };
            }
        }

    }
}
