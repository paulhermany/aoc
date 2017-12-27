using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Hermany.AoC._2017._24
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var components = input.Select((line, i) =>
            {
                var tokens = line.Split('/');
                return new Component {Id = i, In = int.Parse(tokens[0]), Out = int.Parse(tokens[1])};
            }).ToList();

            var bridges = components.Where(component => component.In == 0)
                .Select(component => new List<Component> {component});

            var inprogress = new Queue<List<Component>>();
            foreach (var bridge in bridges)
                inprogress.Enqueue(bridge);
    
            var completed = new List<List<Component>>();

            while (inprogress.Count > 0)
            {
                var current = inprogress.Dequeue();

                var steps = components.Where(
                    component => 
                    (component.In == current.Last().Out || component.Out == current.Last().Out)
                    && current.All(_ => _.Id != component.Id)
                ).ToArray();

                if(steps.Length == 0)
                    completed.Add(current);

                foreach (var step in steps)
                {
                    var list = new List<Component>();
                    list.AddRange(current);
                    list.Add(step.AttachTo(current.Last()));
                    inprogress.Enqueue(list);
                }
            }

            var maxStrength = completed.Max(bridge => bridge.Sum(component => component.Strength));

            return maxStrength.ToString();
        }
        
        public string Part2(params string[] input)
        {
            var components = input.Select((line, i) =>
            {
                var tokens = line.Split('/');
                return new Component { Id = i, In = int.Parse(tokens[0]), Out = int.Parse(tokens[1]) };
            }).ToList();

            var bridges = components.Where(component => component.In == 0)
                .Select(component => new List<Component> { component });

            var inprogress = new Queue<List<Component>>();
            foreach (var bridge in bridges)
                inprogress.Enqueue(bridge);

            var completed = new List<List<Component>>();

            while (inprogress.Count > 0)
            {
                var current = inprogress.Dequeue();

                var steps = components.Where(
                    component =>
                        (component.In == current.Last().Out || component.Out == current.Last().Out)
                        && current.All(_ => _.Id != component.Id)
                ).ToArray();

                if (steps.Length == 0)
                    completed.Add(current);

                foreach (var step in steps)
                {
                    var list = new List<Component>();
                    list.AddRange(current);
                    list.Add(step.AttachTo(current.Last()));
                    inprogress.Enqueue(list);
                }
            }

            var longestStrongest = completed.Select(_ =>
                    new {Length = _.Count, Strength = _.Sum(component => component.Strength)}
                )
                .OrderByDescending(_ => _.Length)
                .ThenByDescending(_ => _.Strength)
                .First();
            
            return longestStrongest.Strength.ToString();
        }
    }
}
