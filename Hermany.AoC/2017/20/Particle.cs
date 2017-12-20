using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2017._20
{
    public class Particle
    {
        public int Index { get; set; }
        public Coord3d P { get; set; }
        public Coord3d V { get; set; }
        public Coord3d A { get; set; }
        public long Distance => Math.Abs(P.X) + Math.Abs(P.Y) + Math.Abs(P.Z);
        public bool IsLive { get; set; } = true;
        public void Update()
        {
            if (!IsLive) return;

            V.X += A.X;
            V.Y += A.Y;
            V.Z += A.Z;

            P.X += V.X;
            P.Y += V.Y;
            P.Z += V.Z;
        }
    }
}
