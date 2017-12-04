using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2016._02
{
    public class Node
    {
        public Dictionary<char, Node> Nodes { get; set; }
        public char Value { get; set; }
        public Node(char value)
        {
            this.Value = value;
            this.Nodes = new Dictionary<char, Node>();
            SetNodes(null, null, null, null);
        }

        public void SetNodes(Node up, Node down, Node left, Node right)
        {
            this.Nodes['U'] = up;
            this.Nodes['D'] = down;
            this.Nodes['L'] = left;
            this.Nodes['R'] = right;

            if (null != up) up.Nodes['D'] = this;
            if (null != down) down.Nodes['U'] = this;
            if (null != left) left.Nodes['R'] = this;
            if (null != right) right.Nodes['L'] = this;
        }
    }
}
