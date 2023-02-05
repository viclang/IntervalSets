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
        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, IntervalType boundaryType, int step)
            => Canonicalize(source, boundaryType, b => b.AddDays(step), b => b.AddDays(-step));

        [Pure]
        public static Interval<DateOnly> Closure(this Interval<DateOnly> source, int step)
            => ToClosed(source, start => start.AddDays(step), end => end.AddDays(-step));

        [Pure]
        public static Interval<DateOnly> Interior(this Interval<DateOnly> source, int step)
            => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));
    }
}
