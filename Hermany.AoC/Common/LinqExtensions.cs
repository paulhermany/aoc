using System.Collections.Generic;
using System.Linq;

namespace Hermany.AoC.Common
{
    public static class LinqExtensions
    {
        public static int CaptchaSum(this IEnumerable<int> values, int compareIndexOffset = 1)
        {
            var arr = values.ToArray();
            return arr.Where((t, i) => t == arr[(i + compareIndexOffset) % arr.Length]).Sum();
        }
    }
}
