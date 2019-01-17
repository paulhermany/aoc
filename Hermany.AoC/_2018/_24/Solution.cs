using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._24
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var groups = ParseGroups(input);
            
            while (groups.GroupBy(_ => _.IsInfection).Select(_ => _.Sum(g => g.Units)).All(_ => _ != 0))
            {
                foreach (var group in groups.Where(_ => _.Units > 0).OrderByDescending(_ => _.EffectivePower)
                    .ThenByDescending(_ => _.Initiative))
                {
                    var attacker = group;

                    var target = groups.Where(_ =>
                            _.Units > 0 &&
                            _.IsInfection != attacker.IsInfection &&
                            !_.IsTargeted &&
                            _.DamageTakenFrom(attacker) > 0
                        )
                        .OrderByDescending(_ => _.DamageTakenFrom(attacker))
                        .ThenByDescending(_ => _.EffectivePower)
                        .ThenByDescending(_ => _.Initiative)
                        .FirstOrDefault();

                    if (null != target)
                    {
                        attacker.Target = target;
                        attacker.Target.IsTargeted = true;
                    }
                }


                foreach (var group in groups.OrderByDescending(_ => _.Initiative))
                {
                    group.Attack();
                }

                foreach (var group in groups)
                {
                    group.IsTargeted = false;
                    group.Target = null;
                }
            }

            var winningUnitCount =
                groups.GroupBy(_ => _.IsInfection).Select(_ => _.Sum(g => g.Units)).Single(_ => _ != 0);

            return winningUnitCount.ToString();
        }

        public string Part2(params string[] input)
        {
            var groups = ParseGroups(input);

            foreach (var group in groups.Where(_ => !_.IsInfection))
            {
                group.AttackStrength += 119;
            }

            while (groups.GroupBy(_ => _.IsInfection).Select(_ => _.Sum(g => g.Units)).All(_ => _ != 0))
            {
                foreach (var group in groups.Where(_ => _.Units > 0).OrderByDescending(_ => _.EffectivePower)
                    .ThenByDescending(_ => _.Initiative))
                {
                    var attacker = group;

                    var target = groups.Where(_ =>
                            _.Units > 0 &&
                            _.IsInfection != attacker.IsInfection &&
                            !_.IsTargeted &&
                            _.DamageTakenFrom(attacker) > 0
                        )
                        .OrderByDescending(_ => _.DamageTakenFrom(attacker))
                        .ThenByDescending(_ => _.EffectivePower)
                        .ThenByDescending(_ => _.Initiative)
                        .FirstOrDefault();

                    if (null != target)
                    {
                        attacker.Target = target;
                        attacker.Target.IsTargeted = true;
                    }
                }


                foreach (var group in groups.OrderByDescending(_ => _.Initiative))
                {
                    group.Attack();
                }

                foreach (var group in groups)
                {
                    group.IsTargeted = false;
                    group.Target = null;
                }
            }

            //var infectionTotal = groups.Where(_ => _.IsInfection).Sum(_ => _.Units);
            //var immuneSystemTotal = groups.Where(_ => !_.IsInfection).Sum(_ => _.Units);

            var winningUnitCount =
                groups.GroupBy(_ => _.IsInfection).Select(_ => _.Sum(g => g.Units)).Single(_ => _ != 0);

            return winningUnitCount.ToString();
        }

        private List<Group> ParseGroups(string[] input)
        {
            var regex = new Regex(@"^(\d+) units each with (\d+) hit points (?:\(((?:\w+ to (?:\w+(?:, )?)+(?:; )?)+)\) )?with an attack that does (\d+) (\w+) damage at initiative (\d+)$", RegexOptions.Compiled);

            var groups = new List<Group>();

            var current = string.Empty;

            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line)) continue;

                if (line == "Immune System:" || line == "Infection:")
                {
                    current = line;
                    continue;
                }

                var match = regex.Match(line);

                var weaknesses = new string[] { };
                var immunities = new string[] { };

                var traitTokens = match.Groups[3].Value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var traitToken in traitTokens)
                {
                    var t = traitToken.Trim();
                    if (t.StartsWith("immune to"))
                    {
                        immunities = t.Substring(9).Split(',').Select(s => s.Trim()).ToArray();
                    }

                    if (t.StartsWith("weak to "))
                    {
                        weaknesses = t.Substring(8).Split(',').Select(s => s.Trim()).ToArray();
                    }
                }

                var group = new Group
                {
                    IsInfection = (current == "Infection:"),
                    Units = int.Parse(match.Groups[1].Value),
                    HP = int.Parse(match.Groups[2].Value),
                    AttackStrength = int.Parse(match.Groups[4].Value),
                    AttackType = match.Groups[5].Value,
                    Initiative = int.Parse(match.Groups[6].Value),
                    Immunities = immunities,
                    Weaknesses = weaknesses
                };

                groups.Add(group);

            }

            return groups;
        }
    }
    

    public class Group
    {
        public bool IsInfection { get; set; }
        public int Units { get; set; }
        public int HP { get; set; }
        public string AttackType { get; set; }
        public int AttackStrength { get; set; }
        public int Initiative { get; set; }
        public string[] Immunities { get; set; }
        public string[] Weaknesses { get; set; }
        public int EffectivePower => Units * AttackStrength;

        public Group Target = null;
        public bool IsTargeted = false;

        public int DamageTakenFrom(Group attacker)
        {
            if (Immunities.Contains(attacker.AttackType)) return 0;
            if (Weaknesses.Contains(attacker.AttackType)) return attacker.EffectivePower * 2;
            return attacker.EffectivePower;
        }
        
        public void Attack()
        {
            if (Units == 0) return;

            if (Target == null) return;

            decimal damageTaken = Target.DamageTakenFrom(this);
            
            var unitsKilled = Math.Floor(damageTaken / Target.HP);

            Target.Units -= (int)unitsKilled;
            if (Target.Units < 0) Target.Units = 0;   
        }
    }

}
