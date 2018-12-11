using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._07
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var instructions = GetInstructions(input);
            
            var sb = new StringBuilder();

            while (instructions.Count > 0)
            {
                var step = instructions.OrderBy(_ => _.Name).ToList().First();
                instructions.Remove(step);
                step.Completed = true;
                sb.Append(step.Name);

                instructions.AddRange(
                    step.Dependents
                        .OrderBy(_ => _.Name)
                        .Where(dependent => dependent.IsAvailable())
                );
            }
            
            return sb.ToString();
        }

        public string Part2(params string[] input)
        {
            var instructions = GetInstructions(input);
            
            var activeSteps = new List<Step>();
            var maxActiveSteps = 5;

            var time = 0;

            while (instructions.Count > 0 || activeSteps.Count > 0)
            {
                for (var i = activeSteps.Count - 1; i >= 0; i--)
                {
                    if (activeSteps[i].TimeRemaining > 0)
                    {
                        activeSteps[i].TimeRemaining--;
                    }

                    if (activeSteps[i].TimeRemaining == 0)
                    {
                        activeSteps[i].Completed = true;
                        instructions.AddRange(
                            activeSteps[i].Dependents
                                .OrderBy(_ => _.Name)
                                .Where(dependent => dependent.IsAvailable())
                        );
                        activeSteps.RemoveAt(i);
                    }
                }

                var availableWorkers = maxActiveSteps - activeSteps.Count;

                for (int i = 0; i < availableWorkers; i++)
                {
                    if (instructions.Count > 0)
                    {
                        var step = instructions.OrderBy(_ => _.Name).ToList().First();
                        instructions.Remove(step);
                        activeSteps.Add(step);
                    }
                }

                time++;
            }

            return (time - 1).ToString();
        }
        
        public List<Step> GetInstructions(string[] input)
        {
            var regex = new Regex(@"Step ([A-Z]) must be finished before step ([A-Z]) can begin\.", RegexOptions.Compiled);

            var steps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().ToDictionary(_ => _, _ => new Step(_));

            foreach (var line in input)
            {
                var match = regex.Match(line);
                steps[Convert.ToChar(match.Groups[1].Value)].Dependents.Add(steps[Convert.ToChar(match.Groups[2].Value)]);
                steps[Convert.ToChar(match.Groups[2].Value)].Dependencies.Add(steps[Convert.ToChar(match.Groups[1].Value)]);
            }

            var instructions = new List<Step>();
            instructions.AddRange(steps.Values.Where(_ => _.Dependencies.Count == 0 && _.Dependents.Count != 0));

            return instructions;
        }
    }
    
    public class Step
    {
        public char Name { get; set; }
        public List<Step> Dependencies { get; set; }
        public List<Step> Dependents { get; set; }
        public bool Completed { get; set; }
        public int TimeRemaining { get; set; }

        public Step(char name)
        {
            Name = name;
            Dependencies = new List<Step>();
            Dependents = new List<Step>();
            Completed = false;
            TimeRemaining = Convert.ToInt32(name) - 4;
        }

        public bool IsAvailable()
        {
            return Dependencies.All(_ => _.Completed);
        }
    }
}
