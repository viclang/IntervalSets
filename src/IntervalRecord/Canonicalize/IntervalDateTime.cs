using InfinityComparable;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, IntervalType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));

        [Pure]
        public static Interval<DateTime> Closure(this Interval<DateTime> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        [Pure]
        public static Interval<DateTime> Interior(this Interval<DateTime> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));
    }
}
