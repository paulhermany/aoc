using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2017._09
{
    public class Node
    {
        public List<Node> Children;
        public Node Parent { get; set; }

        public int Score => Parent?.Score + 1 ?? 1;

        public int TotalScore => Score + Children.Sum(_ => _.TotalScore);

        public Node()
        {
             Children = new List<Node>();
        }
    }
}
