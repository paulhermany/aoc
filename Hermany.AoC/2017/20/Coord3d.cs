using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2017._20
{
    public class Coord3d
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }

        public override string ToString() =>
            $"{X},{Y},{Z}";
    }
}
