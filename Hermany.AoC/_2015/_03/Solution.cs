using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC.Common;

namespace Hermany.AoC._2015._03
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            return GetNumberOfVisitedHouses(input.Single()).ToString();
        }

        public string Part2(params string[] input)
        {
            return GetNumberOfVisitedHouses(input.Single(), 2).ToString();
        }
        
        public int GetNumberOfVisitedHouses(string val, int numberOfAgents = 1, char north = '^', char south = 'v', char east = '>', char west = '<')
        {
            var houses = new Dictionary<Tuple<int, int>, int>
            {
                {new Tuple<int, int>(0, 0), 1}
            };

            var agents = Enumerable.Range(0, numberOfAgents)
                .Select(_ => new Point {X = 0, Y = 0})
                .ToArray();

            var agentIndex = 0;

            foreach (var c in val)
            {
                if (c == north) agents[agentIndex].Y++;
                if (c == south) agents[agentIndex].Y--;
                if (c == east) agents[agentIndex].X++;
                if (c == west) agents[agentIndex].X--;

                var t = new Tuple<int, int>(agents[agentIndex].X, agents[agentIndex].Y);

                if (!houses.ContainsKey(t)) houses.Add(t, 0);
                houses[t]++;

                agentIndex = (agentIndex + 1) % numberOfAgents;
            }

            return houses.Keys.Count;
        }

    }
}
