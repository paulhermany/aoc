using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hermany.AoC._2017._12
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var programs = new Dictionary<int, HashSet<int>>();

            foreach (var line in input)
            {
                var tokens = line.Split(new[] {" <-> "}, StringSplitOptions.None);

                var lhs = int.Parse(tokens[0]);
                var rhs = tokens[1].Split(',').Select(_ => int.Parse(_.Trim()));

                if(!programs.ContainsKey(lhs))
                    programs.Add(lhs, new HashSet<int>());

                foreach (var id in rhs)
                {
                    if(!programs.ContainsKey(id))
                        programs.Add(id, new HashSet<int>());
                    if (!programs[id].Contains(lhs)) programs[id].Add(lhs);
                }
            }

            var check = new Stack<int>();
            check.Push(0);

            var @checked = new HashSet<int>();

            while (check.Count > 0)
            {
                var current = check.Pop();
                @checked.Add(current);

                var toChecks = programs.Where(_ =>
                    !@checked.Contains(_.Key) && (_.Key == current || _.Value.Contains(current)));

                foreach (var toCheck in toChecks)
                    check.Push(toCheck.Key);
            }

            return @checked.Count().ToString();
        }
        
        public string Part2(params string[] input)
        {
            var programs = new Dictionary<int, HashSet<int>>();

            foreach (var line in input)
            {
                var tokens = line.Split(new[] { " <-> " }, StringSplitOptions.None);

                var lhs = int.Parse(tokens[0]);
                var rhs = tokens[1].Split(',').Select(_ => int.Parse(_.Trim()));

                if (!programs.ContainsKey(lhs))
                    programs.Add(lhs, new HashSet<int>());

                foreach (var id in rhs)
                {
                    if (!programs.ContainsKey(id))
                        programs.Add(id, new HashSet<int>());
                    if (!programs[id].Contains(lhs)) programs[id].Add(lhs);
                }
            }

            var @checked = new HashSet<int>();

            var groups = 0;

            foreach (var key in programs.Keys)
            {
                if (!@checked.Contains(key))
                {
                    groups++;

                    var check = new Stack<int>();
                    check.Push(key);
                    
                    while (check.Count > 0)
                    {
                        var current = check.Pop();
                        @checked.Add(current);

                        var toChecks = programs.Where(_ =>
                            !@checked.Contains(_.Key) && (_.Key == current || _.Value.Contains(current)));

                        foreach (var toCheck in toChecks)
                            check.Push(toCheck.Key);
                    }
                }
            }

            return groups.ToString();
        }
    }
}
