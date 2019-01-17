using System;
using System.Collections.Generic;
using System.Linq;
using Hermany.AoC.Common;
using Math = System.Math;

namespace Hermany.AoC._2018._25
{
    public class Solution : ISolution
    {
        public List<List<(int X, int Y, int Z, int T)>> groups = new List<List<(int X, int Y, int Z, int T)>>();
        public string Part1(params string[] input)
        {
            var points = input.Select(_ =>
            {
                var tokens = _.Split(',');
                return (X: int.Parse(tokens[0]), Y: int.Parse(tokens[1]), Z: int.Parse(tokens[2]), T: int.Parse(tokens[3]));
            });

            foreach(var point in points)
            {
                var added = false;

                foreach (var group in groups)
                {
                    var c = group.Count();

                    foreach (var member in group)
                    {
                        if (ManhattanDistance(member, point) <= 3)
                        {
                            added = true;
                            group.Add(point);

                            //var otherGroups = groups.Where(_ => _ != group);

                            //foreach (var otherGroup in otherGroups)
                            //{
                            //    var connect = false;
                            //    foreach (var otherGroupMember in otherGroup)
                            //    {
                            //        if (ManhattanDistance(otherGroupMember, point) <= 3)
                            //        {
                            //            connect = true;
                            //            break;
                            //        }
                            //    }

                            //    if (connect)
                            //    {
                            //        group.AddRange(otherGroup);
                            //        otherGroup.Clear();
                            //    }
                            //}

                            var membersOfOtherGroups = groups.Where(_ => _ != group)
                                .SelectMany(_ => _.Select(m => new { Group = _, Member = m })).ToArray();

                            foreach (var memberOfOtherGroups in membersOfOtherGroups)
                            {
                                if (ManhattanDistance(point, memberOfOtherGroups.Member) <= 3)
                                {
                                    if (memberOfOtherGroups.Group.Count > 0)
                                    {
                                        group.AddRange(memberOfOtherGroups.Group);
                                        memberOfOtherGroups.Group.Clear();
                                    }
                                }
                            }

                            break;
                        }
                    }

                    if (group.Count > c)
                        break;
                }

                if (!added)
                    groups.Add(new List<(int X, int Y, int Z, int T)> {point});
            }


            // 606 too high
            return groups.Count(_ => _.Count > 0).ToString();
        }

        public string Part2(params string[] input)
        {
            return string.Empty;
        }

        private int ManhattanDistance((int X, int Y, int Z, int T) p1, (int X, int Y, int Z, int T) p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y) + Math.Abs(p1.Z - p2.Z) + Math.Abs(p1.T - p2.T);
        }
    }
}
