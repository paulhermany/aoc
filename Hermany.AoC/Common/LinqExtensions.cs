using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hermany.AoC.Common
{
    public static class LinqExtensions
    {
        public static IEnumerable<string> Pivot(this IEnumerable<string> obj) => obj
            .SelectMany(_ => _.Select((value, index) => new {Value = value, Index = index}))
            .GroupBy(_ => _.Index)
            .Select(grp => string.Concat(grp.Select(_ => _.Value).ToArray()));
    }
}
