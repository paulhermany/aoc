using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2018._06
{
    public class Manhattan
    {
        public static int ManhattanDistance(ValueTuple<int, int> a, ValueTuple<int, int> b)
            => Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);

        public static IEnumerable<ValueTuple<int, int>> ManhattanWalk(ValueTuple<int, int>? start)
        {
            var theta = 7 * Math.PI / 4;
            const double delta = -1 * Math.PI / 2;
            var distance = 0;

            var p = start ?? new ValueTuple<int, int>(0, 0);

            while (true)
            {
                if (0 == distance)
                    yield return p;

                for (var i = 0; i < 4; i++)
                {
                    for (var r = 0; r < distance; r++)
                    {
                        p.Item1 += (int)Math.Round(Math.Cos(theta));
                        p.Item2 += (int)Math.Round(Math.Sin(theta));

                        yield return p;
                    }

                    theta += delta;
                }

                distance++;
                p.Item2++;
            }
        }

    }
}
