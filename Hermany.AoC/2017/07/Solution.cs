using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC._2017._07
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var nodes = new List<Node>();
            foreach (var line in input)
                nodes.Add(Node.Parse(line));

            foreach (var node in nodes)
                node.AttachChildren(nodes);

            var root = nodes.Single(_ => null == _.Parent);

            return root.Name;
        }

        public string Part2(params string[] input)
        {
            var nodes = new List<Node>();
            foreach (var line in input)
                nodes.Add(Node.Parse(line));

            foreach (var node in nodes)
                node.AttachChildren(nodes);
            
            var root = nodes.Single(_ => null == _.Parent);
            
            var unbalancedNode = nodes.Where(_ => _.HasUnbalancedTowers()).OrderByDescending(_ => _.Depth).First();
            
            var towerWeights = unbalancedNode.Children.Values.Select(_ => _.TowerWeight);
            var towerWeightsMin = towerWeights.Min();
            var towerWeightsMax = towerWeights.Max();
            if (towerWeightsMin != towerWeightsMax)
            {
                var towerWeightsAtMinCount = unbalancedNode.Children.Values.Where(_ => _.TowerWeight == towerWeightsMin).Count();
                var towerWeightsAtMaxCount = unbalancedNode.Children.Values.Where(_ => _.TowerWeight == towerWeightsMax).Count();

                if (towerWeightsAtMinCount > towerWeightsAtMaxCount)
                {
                    var unbalanced = unbalancedNode.Children.Values.Single(_ => _.TowerWeight == towerWeightsMax);
                    return (unbalanced.Weight - (unbalanced.TowerWeight - towerWeightsMin)).ToString();
                }
                else
                {
                    var unbalanced = unbalancedNode.Children.Values.Single(_ => _.TowerWeight == towerWeightsMin);
                    return (unbalanced.Weight + (towerWeightsMax - unbalanced.TowerWeight)).ToString();
                }
            }

            return "0";
        }

    }
}
