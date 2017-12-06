using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hermany.AoC._2016._07
{
    public class IPv7Address
    {
        protected readonly string Value;

        public IEnumerable<string> HypernetSequences =>
            _hypernetSequences ?? (_hypernetSequences = Regex.Matches(Value, @"\[(\w+)\]").Cast<Match>()
                .Select(_ => _.Groups[1].Value).ToList());

        public IEnumerable<string> SupernetSequences =>
            _supernetSequences ?? (_supernetSequences = HypernetSequences
                .Aggregate(Value,
                    (current, hypernetSequence) => current.Replace(hypernetSequence, string.Empty))
                .Split(new[] {"[]"}, StringSplitOptions.RemoveEmptyEntries).ToList());

        public IPv7Address(string value)
        {
            Value = value;
        }

        private IEnumerable<string> _hypernetSequences;
        private IEnumerable<string> _supernetSequences;
    }
}
