using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Hermany.AoC.Common;

namespace Hermany.AoC._2018._20
{
    public class Solution : ISolution
    {
        public string Part1(params string[] input)
        {
            var map = new Map {{(0, 0), 'X'}};

            //var directions = "^WSSEESWWWNW(S|NENNEEEENN(ESSSSW(NWSW|SSEN)|WSWWN(E|WWS(E|SS))))$";
            var directions = input[0];
            var queue = new Queue<char>(directions.Skip(1).Take(directions.Length - 2));
   
            var stack = new Stack<(int,int)>();
            stack.Push((0, 0));

 
            while (queue.Count > 0)
            {
                //Console.SetCursorPosition(0,0);
                //Console.Write(map.Print());
                //System.Threading.Thread.Sleep(100);

                var current = stack.Pop();
                
                var c = queue.Dequeue();

                switch (c)
                {
                    case '(':
                        stack.Push(current);
                        stack.Push(current);
                        break;
                    case ')':
                        break;
                    case '|':
                        stack.Push(stack.Peek());
                        break;
                    case 'N':
                        map.Add((current.Item1, current.Item2 - 1), '-');
                        current = (current.Item1, current.Item2 - 2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'S':
                        map.Add((current.Item1, current.Item2 + 1), '-');
                        current = (current.Item1, current.Item2 + 2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'E':
                        map.Add((current.Item1 + 1, current.Item2), '|');
                        current = (current.Item1 + 2, current.Item2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'W':
                        map.Add((current.Item1 - 1, current.Item2), '|');
                        current = (current.Item1 - 2, current.Item2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    default:
                        break;
                }
            }

            var scores = new Dictionary<(int,int), int>();

            var maxScore = 0;

            var targets = map.Where(_ => _.Value == '.').OrderBy(_ => _.Key.Item2).ThenBy(_ => _.Key.Item1).ToArray();

            foreach (var target in targets)
            {
                if (scores.ContainsKey(target.Key))
                    continue;

                //Console.SetCursorPosition(0,0);
                //Console.Write(map.Print());
                //System.Threading.Thread.Sleep(100);

                var path = map.FindPath(new Node((0, 0)), new Node(target.Key));

                if (path.F >= maxScore)
                    maxScore = path.F;

                var _current = path;
                while (_current != null)
                {
                    //map.Draw(_current.Position.Item1, _current.Position.Item2, '_');
                    //System.Threading.Thread.Sleep(100);

                    if (!scores.ContainsKey(_current.Position))
                        scores.Add(_current.Position, _current.F);

                    _current = _current.Parent;
                }
            }

            //var node = map.FindPath(new Node((0, 0)), new Node((0, -6)));

            //var _current = node;
            //while (_current != null)
            //{
            //    map.Draw(_current.Position.Item1, _current.Position.Item2, 'o');
            //    System.Threading.Thread.Sleep(1000);
            //    _current = _current.Parent;
            //}

            return maxScore.ToString();
        }

        public string Part2(params string[] input)
        {
            var map = new Map { { (0, 0), 'X' } };

            var directions = input[0];
            var queue = new Queue<char>(directions.Skip(1).Take(directions.Length - 2));

            var stack = new Stack<(int, int)>();
            stack.Push((0, 0));


            while (queue.Count > 0)
            {
                var current = stack.Pop();

                var c = queue.Dequeue();

                switch (c)
                {
                    case '(':
                        stack.Push(current);
                        stack.Push(current);
                        break;
                    case ')':
                        break;
                    case '|':
                        stack.Push(stack.Peek());
                        break;
                    case 'N':
                        map.Add((current.Item1, current.Item2 - 1), '-');
                        current = (current.Item1, current.Item2 - 2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'S':
                        map.Add((current.Item1, current.Item2 + 1), '-');
                        current = (current.Item1, current.Item2 + 2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'E':
                        map.Add((current.Item1 + 1, current.Item2), '|');
                        current = (current.Item1 + 2, current.Item2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    case 'W':
                        map.Add((current.Item1 - 1, current.Item2), '|');
                        current = (current.Item1 - 2, current.Item2);
                        map.Add(current, '.');
                        stack.Push(current);
                        break;
                    default:
                        break;
                }
            }

            var roomsWithTarget = 0;

            var targetScore = 1000;

            var targets = map.Where(_ => _.Value == '.').OrderBy(_ => _.Key.Item2).ThenBy(_ => _.Key.Item1).ToArray();

            foreach (var target in targets)
            {
                var path = map.FindPath(new Node((0, 0)), new Node(target.Key));

                if (path.F >= targetScore)
                    roomsWithTarget++;
            }

            return roomsWithTarget.ToString();
        }
    }
    
    public class Map : Dictionary<(int,int), char>
    {
        private int minX = 0;
        private int maxX = 0;
        private int minY = 0;
        private int maxY = 0;

        public new void Add((int, int) position, char c)
        {
            if (!ContainsKey(position))
            {
                base.Add(position, c);

                if (position.Item1 < minX) minX = position.Item1;
                if (position.Item1 > maxX) maxX = position.Item1;
                if (position.Item2 < minY) minY = position.Item2;
                if (position.Item2 > maxY) maxY = position.Item2;
            }
        }

        public string Print()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < maxY - minY + 1; y++)
            {
                for (var x = 0; x < maxX - minX + 1; x++)
                {
                    var position = (x + minX, y + minY);
                    sb.Append(ContainsKey(position) ? this[position] : '#');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Draw(int x, int y, char c)
        {
            Console.SetCursorPosition(x - minX, y - minY);
            Console.Write(c);
        }

        public Node FindPath(Node start, Node target)
        {
            Node current = null;
            var openList = new Dictionary<(int,int),Node>();
            var closedList = new HashSet<(int,int)>();
            int g = 0;

            openList.Add(start.Position, start);

            while (openList.Count > 0)
            {
                var lowest = openList.Values.Min(_ => _.F);
                current = openList.Values.First(_ => _.F == lowest);
                
                closedList.Add(current.Position);

                openList.Remove(current.Position);

                if (closedList.Contains(target.Position))
                    break;

                var adjacentLocations = GetOpenAdjacentNodes(current, openList);
                
                g = current.G + 1;

                foreach (var adjacentLocation in adjacentLocations)
                {
                    if (closedList.Contains(adjacentLocation.Position))
                        continue;

                    if (!openList.ContainsKey(adjacentLocation.Position))
                    {
                        adjacentLocation.G = g;
                        adjacentLocation.H = ComputeHScore(adjacentLocation.Position, target.Position);
                        adjacentLocation.F = adjacentLocation.G + adjacentLocation.H;
                        adjacentLocation.Parent = current;

                        openList.Add(adjacentLocation.Position, adjacentLocation);
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

            if (null != current && current.Position.Item1 == target.Position.Item1 && current.Position.Item2 == target.Position.Item2)
                return current;

            return null;
        }

        private static int ComputeHScore((int,int) position, (int,int) target)
        {
            return Math.Abs(target.Item1 - position.Item1) + Math.Abs(target.Item2 - position.Item2);
        }

        private IEnumerable<Node> GetOpenAdjacentNodes(Node current, Dictionary<(int,int), Node> open)
        {
            var nodes = new List<Node>();
            
            var N = (current.Position.Item1, current.Position.Item2 - 1);
            var N_1 = (current.Position.Item1, current.Position.Item2 - 2);
            if (this.ContainsKey(N) && this[N] == '-' && !open.ContainsKey(N_1))
                nodes.Add(new Node(N_1));

            var S = (current.Position.Item1, current.Position.Item2 + 1);
            var S_1 = (current.Position.Item1, current.Position.Item2 + 2);
            if (this.ContainsKey(S) && this[S] == '-' && !open.ContainsKey(S_1))
                nodes.Add(new Node(S_1));

            var E = (current.Position.Item1 - 1, current.Position.Item2);
            var E_1 = (current.Position.Item1 - 2, current.Position.Item2);
            if (this.ContainsKey(E) && this[E] == '|' && !open.ContainsKey(E_1))
                nodes.Add(new Node(E_1));

            var W = (current.Position.Item1 + 1, current.Position.Item2);
            var W_1 = (current.Position.Item1 + 2, current.Position.Item2);
            if (this.ContainsKey(W) && this[W] == '|' && !open.ContainsKey(W_1))
                nodes.Add(new Node(W_1));

            return nodes;
        }
    }

    public class Node
    {
        public (int,int) Position { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }

        public Node Parent { get; set; }

        public Node((int, int) position)
        {
            this.Position = position;
        }
    }
}
