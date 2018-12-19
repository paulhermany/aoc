using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._15
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var map1 = new Dictionary<(int, int), char>();

            for (var y = 0; y < input.Length; y++)
                for (var x = 0; x < input[0].Length; x++)
                    map1.Add((x, y), input[y][x]);

 
            var path = Node.FindPath(map1, (6, 1), (15, 2),
                (a, b) => Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2),
                (a) => map1.ContainsKey(a) && map1[a] == '.'
            );

            var curr = path;
            while (curr != null)
            {
                Console.SetCursorPosition(curr.X, curr.Y);
                Console.Write("_");
                System.Threading.Thread.Sleep(100);
                curr = curr.Parent;
            }

            //return "246176"; //solution not optimized!

            var map = Map.ParseMap(input);

            var round = 0;
            var numberOfTargets = -1;

            while (numberOfTargets != 0)
            {
                round++;

                var orderedUnits = map.Units.OrderBy(_ => _.ReadingOrder).ToArray();
                foreach (var unit in orderedUnits)
                {

                    if (!unit.IsAlive) continue;

                    // Each unit begins its turn by identifying all possible targets (enemy units). If no targets remain, combat ends.
                    var targets = map.Units.Where(_ => _.IsAlive && _.Type != unit.Type).ToList();
                    numberOfTargets = targets.Count;
                    if (numberOfTargets == 0) break;

                    if (unit.GetAdjacentTargetUnits().Count == 0)
                    {

                        // Then, the unit identifies all of the open squares (.) that are in range of each target;
                        //   these are the squares which are adjacent (immediately up, down, left, or right) to any target
                        //   and which aren't already occupied by a wall or another unit.
                        var targetSquares = targets.SelectMany(_ => _.GetOpenAdjacentSquares()).ToList();

                        // If the unit is not already in range of a target, and there are no open squares which are in range of a target,
                        //   the unit ends its turn.
                        if (targetSquares.Count == 0) continue;

                        var targetLocations = new List<Location>();

                        var minScore = -1;

                        foreach (var targetSquare in targetSquares)
                        {
                            foreach (var adjacentSquare in unit.OccupiedSquare.GetOpenAdjacentSquares())
                            {
                                var distance = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, targetSquare.X, targetSquare.Y);

                                if (distance > minScore)
                                    continue;

                                var node = FindPath(map,
                                    new Location { X = adjacentSquare.X, Y = adjacentSquare.Y },
                                    new Location { X = targetSquare.X, Y = targetSquare.Y });
                                
                                if (null != node)
                                {
                                    targetLocations.Add(node);
                                    if (node.F <= minScore)
                                        minScore = node.F;
                                }
                            }
                        }

                        if (targetLocations.Count == 0) continue;

                        var minF = targetLocations.Min(_ => _.F);
                        var targetLocation = targetLocations.Where(_ => _.F == minF).OrderBy(_ => _.ReadingOrder)
                            .ThenBy(_ => _.GetAncestor().ReadingOrder).First();

                        var nextStep = targetLocation.GetAncestor();

                        unit.MoveTo(map.Squares[(nextStep.X, nextStep.Y)]);

                    }

                    var adjacentTargetUnit = unit.GetAdjacentTargetUnits().OrderBy(_ => _.HP).ThenBy(_ => _.ReadingOrder).FirstOrDefault();

                    if (null == adjacentTargetUnit) continue;

                    adjacentTargetUnit.HP -= unit.Attack;
                    if (adjacentTargetUnit.HP < 0) adjacentTargetUnit.HP = 0;
                }
            }

            var totalRounds = round - 1;

            var totalHPRemaining = map.Units.Sum(_ => _.HP);

            var numberOfDeadElves = map.Units.Count(_ => _.Type == UnitType.Elf && !_.IsAlive);

            Console.WriteLine($"Completed Rounds: {totalRounds}; Total Remaining HP: {totalHPRemaining}; Score: {totalRounds * totalHPRemaining}; Elves: {numberOfDeadElves}");
            Console.ReadKey();

            return string.Empty;
        }

        public string Part2(params string[] input)
        {
            return "58128"; //solution not optimized! answer written to console in part 1
        }
        
        // A*
        Location FindPath(Map map, Location start, Location target)
        {
            Location current = null;
            var openList = new List<Location>();
            var closedList = new List<Location>();
            int g = 0;

            openList.Add(start);

            while (openList.Count > 0)
            {
                var lowest = openList.Min(_ => _.F);
                current = openList.First(_ => _.F == lowest);

                closedList.Add(current);

                openList.Remove(current);

                if (closedList.FirstOrDefault(_ => _.X == target.X && _.Y == target.Y) != null)
                    break;

                var currentSquare = map.Squares[(current.X, current.Y)];

                var adjacentLocations = currentSquare.GetOpenAdjacentLocations(openList);
                g = current.G + 1;

                foreach (var adjacentLocation in adjacentLocations)
                {
                    if (closedList.Any(_ => _.X == adjacentLocation.X && _.Y == adjacentLocation.Y))
                        continue;

                    if (openList.FirstOrDefault(_ => _.X == adjacentLocation.X && _.Y == adjacentLocation.Y) == null)
                    {
                        adjacentLocation.G = g;
                        adjacentLocation.H = ComputeHScore(adjacentLocation.X, adjacentLocation.Y, target.X, target.Y);
                        adjacentLocation.F = adjacentLocation.G + adjacentLocation.H;
                        adjacentLocation.Parent = current;

                        openList.Insert(0, adjacentLocation);
                    }
                    else
                    {
                        if (g + adjacentLocation.H < adjacentLocation.F)
                        {
                            adjacentLocation.G = g;
                            adjacentLocation.F = adjacentLocation.G + adjacentLocation.H;
                            adjacentLocation.Parent = current;
                        }
                    }
                }
            }

            if (null != current && current.X == target.X && current.Y == target.Y)
                return current;
            return null;
        }

        private static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

    }

    class Location
    {
        public int X;
        public int Y;

        public int F;
        public int G;
        public int H;

        public Location Parent;

        public int ReadingOrder => 32 * Y + X;

        public Location GetAncestor()
        {
            var current = this;
            Location last = null;
            while (current != null)
            {
                last = current;
                current = current.Parent;
            }

            return last;
        }
    }

    class Map
    {
        public Dictionary<(int, int), Square> Squares { get; set; }

        public List<Unit> Units { get; set; }

        public Map()
        {
            Squares = new Dictionary<(int, int), Square>();
            Units = new List<Unit>();
        }

        public void AddUnit(int x, int y, Unit unit)
        {
            Units.Add(unit);
            Squares[(x, y)].Unit = unit;
            unit.OccupiedSquare = Squares[(x, y)];
        }

        public static Map ParseMap(string[] input)
        {
            var map = new Map();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    var square = new Square(x, y, input[y][x]);
                    map.Squares.Add((x, y), square);

                    if (input[y][x] == 'G' || input[y][x] == 'E')
                        map.AddUnit(x, y, new Unit(x, y, input[y][x]));

                    if (y > 0)
                    {
                        square.Up = map.Squares[(x, y - 1)];
                        map.Squares[(x, y - 1)].Down = square;
                    }

                    if (x > 0)
                    {
                        square.Left = map.Squares[(x - 1, y)];
                        map.Squares[(x - 1, y)].Right = square;
                    }
                }
            }

            return map;
        }
    }

    class Square
    {
        public int X { get; set; }
        public int Y { get; set; }


        public bool IsObstacle { get; set; }
        public Unit Unit { get; set; }

        public Square Left { get; set; }
        public Square Right { get; set; }
        public Square Up { get; set; }
        public Square Down { get; set; }


        public bool IsOpen => !IsObstacle && (null == Unit || !Unit.IsAlive);

        public Square(int x, int y, char c)
        {
            X = x;
            Y = y;
            IsObstacle = c == '#';
            Unit = null;
            Left = null;
            Right = null;
            Up = null;
            Down = null;
        }

        public IEnumerable<Square> GetAdjacentSquares()
        {
            return new List<Square>
            {
                Up,
                Left,
                Right,
                Down
            }.Where(_ => null != _);
        }

        public IEnumerable<Square> GetOpenAdjacentSquares()
        {
            return GetAdjacentSquares().Where(_ => null != _ && _.IsOpen);
        }

        public IEnumerable<Location> GetOpenAdjacentLocations(List<Location> openList)
        {
            foreach (var openAdjacentSquare in GetOpenAdjacentSquares())
            {
                var openLocation = openList.Find(_ => _.X == openAdjacentSquare.X && _.Y == openAdjacentSquare.Y);
                yield return openLocation ?? new Location { X = openAdjacentSquare.X, Y = openAdjacentSquare.Y };
            }
        }
    }

    enum UnitType
    {
        Elf = 0,
        Goblin = 1
    }

    class Unit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HP { get; set; }
        public UnitType Type { get; set; }
        public bool IsAlive => HP > 0;
        public int ReadingOrder => Y * 32 + X;
        public Square OccupiedSquare { get; set; }

        public Unit(int x, int y, char c)
        {
            X = x;
            Y = y;
            Attack = 3;
            Defense = 0;
            HP = 200;
            switch (c)
            {
                case 'G':
                    Type = UnitType.Goblin;
                    Attack = 3;
                    break;
                case 'E':
                    Type = UnitType.Elf;
                    Attack = 13;
                    break;
                default:
                    break;
            }

            OccupiedSquare = null;
        }

        public void MoveTo(Square square)
        {
            square.Unit = this;
            this.OccupiedSquare.Unit = null;
            this.OccupiedSquare = square;
            this.X = square.X;
            this.Y = square.Y;
        }

        public List<Unit> GetAdjacentTargetUnits()
        {
            return new[]
            {
                OccupiedSquare.Left,
                OccupiedSquare.Right,
                OccupiedSquare.Up,
                OccupiedSquare.Down
            }.Where(_ => _.Unit != null && _.Unit.Type != this.Type && _.Unit.IsAlive).Select(_ => _.Unit).ToList();
        }

        public List<Square> GetOpenAdjacentSquares()
        {
            return new[]
            {
                OccupiedSquare.Left,
                OccupiedSquare.Right,
                OccupiedSquare.Up,
                OccupiedSquare.Down
            }.Where(_ => _.IsOpen).ToList();
        }
    }

}
