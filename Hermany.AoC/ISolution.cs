using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermany.AoC
{
    public interface ISolution
    {
        string Part1(params string[] input);
        string Part2(params string[] input);
    }
}
