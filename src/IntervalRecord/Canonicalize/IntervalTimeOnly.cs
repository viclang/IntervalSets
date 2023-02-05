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
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, IntervalType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));

        [Pure]
        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        [Pure]
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Add(-step));
    }
}
