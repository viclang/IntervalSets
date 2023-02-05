using InfinityComparable;
using IntervalRecord.Internal;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static DateTime? Centre(this Interval<DateTime> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        [Pure]
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> value, BoundaryType boundaryType, TimeSpan step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        [Pure]
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step)
            => IntervalHelper.ToClosed(value, x => x.Add(step), x => x.Add(-step));

        [Pure]
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step)
            => IntervalHelper.ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }
}
