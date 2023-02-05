
using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, IntervalType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));

        [Pure]
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        [Pure]
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));
    }
}
