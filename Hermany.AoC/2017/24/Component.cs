using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC._2017._24
{
    public class Component
    {
        public int Id { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public int Strength => In + Out;
        
        public Component AttachTo(Component component) => 
            In == component.Out ? this : Flip();

        public Component Flip() => new Component
        {
            Id = Id,
            In = Out,
            Out = In
        };
    }
}
