using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._07
{
    public class Node
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public IEnumerable<string> Holding { get; set; }
        public Node Parent { get; set; }
        public IDictionary<string, Node> Children { get; set; } = new Dictionary<string, Node>();

        public int Depth
        {
            get
            {
                if (!_depth.HasValue)
                    _depth = 1 + (Parent?.Depth ?? 0);
                return _depth.Value;
            }
        }
        public int TowerWeight
        {
            get
            {
                if (!_towerWeight.HasValue)
                    _towerWeight = Weight + Children.Values.Sum(child => child.TowerWeight);
                return _towerWeight.Value;
            }
        }
        public void AttachChildren(IEnumerable<Node> nodes)
        {
            if (null == Holding) return;
            foreach (var node in nodes.Where(_ => Holding.Contains(_.Name)))
            {
                Children.Add(node.Name, node);
                node.Parent = this;
            }
        }

        public bool HasUnbalancedTowers()
        {
            if (Children.Count == 0) return false;
            var towerWeights = Children.Values.Select(_ => _.TowerWeight).ToArray();
            return towerWeights.Min() != towerWeights.Max();
        }

        public static Node FindUnbalancedNodeDfs(Node root) =>
            root.Children.Values.Select(FindUnbalancedNodeDfs)
                .FirstOrDefault(unbalanced => null != unbalanced);
        
        public static Node Parse(string line)
        {
            var tokens = line.Split(new[] {"->"}, StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim()).ToArray();
            var nameAndWeightMatch = Regex.Match(tokens[0], @"(\w+)\s?\((\d+)\)");

            var node = new Node
            {
                Name = nameAndWeightMatch.Groups[1].Value,
                Weight = int.Parse(nameAndWeightMatch.Groups[2].Value)
            };

            if (tokens.Length > 1)
                node.Holding = tokens[1].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim()).ToArray();

            return node;
        }

        private int? _depth = null;
        private int? _towerWeight = null;
    }
}
