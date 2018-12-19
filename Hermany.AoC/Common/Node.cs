using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Hermany.AoC._2018._15;

namespace Hermany.AoC.Common
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        
        public Node Parent { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Node FindPath<T>(IDictionary<(int, int), T> map, (int, int) start, (int, int) target,
            Func<(int,int), (int,int), int> heuristic,
            Func<(int,int), bool> isWalkable)
        {

            Node.DrawMap(map, 0, 0, 32, 32);

            Node current = null;
            var openList = new Dictionary<(int, int), Node>();
            var closed = new HashSet<(int,int)>();

            int g = 0;


            openList.Add(start, new Node(start.Item1, start.Item2));

            while (openList.Count > 0)
            {
                var lowest = openList.Values.Min(_ => _.F);
                current = openList.First(_ => _.Value.F == lowest).Value;

                var currentKey = (current.X, current.Y);

                closed.Add(currentKey);

                openList.Remove(currentKey);

                if (current.X == target.Item1 && current.Y == target.Item2)
                    break;
                
                var adjacentNodes = new List<(int, int)>()
                {
                    (current.X, current.Y - 1),
                    (current.X + 1, current.Y),
                    (current.X, current.Y + 1),
                    (current.X - 1, current.Y)
                }.Where(_ => !closed.Contains(_) && !openList.ContainsKey(_) && isWalkable(_))
                .Select(_ => new Node(_.Item1, _.Item2));
                
                g = current.G + 1;

                foreach (var node in adjacentNodes)
                {
                    Console.SetCursorPosition(node.X, node.Y);
                    Console.Write("?");
                    System.Threading.Thread.Sleep(100);

                    if (closed.Contains((node.X, node.Y)))
                        continue;

                    if (!openList.Any(_ => _.Value.X == node.X && _.Value.Y == node.Y))
                    {
                        node.G = g;
                        node.H = heuristic((node.X, node.Y), target);
                        node.F = node.G + node.H;
                        node.Parent = current;
                        
                        Console.SetCursorPosition(current.X, current.Y);
                        Console.Write('!');
                        System.Threading.Thread.Sleep(100);

                        openList.Add((node.X, node.Y), node);
                    }
                    else
                    {
                        if (g + node.H < node.F)
                        {
                            node.G = g;
                            node.F = node.G + node.H;
                            node.Parent = current;

                            Console.SetCursorPosition(current.X, current.Y);
                            Console.Write('^');
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }

            if (null != current && current.X == target.Item1 && current.Y == target.Item2)
                return current;

            return null;
        }

        public static void DrawMap<T>(IDictionary<(int, int), T> map, int x, int y, int width, int height)
        {
            for (var _y = y; _y < y + height && _y - y < Console.BufferWidth; _y++)
            {
                for (var _x = x; _x < x + width && _x - x < Console.BufferHeight; _x++)
                {
                    Console.SetCursorPosition(_x - x, _y - y);
                    Console.Write(map[(_x,_y)]);
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
