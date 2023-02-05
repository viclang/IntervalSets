using InfinityComparable;
using IntervalRecord.Internal;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        public static TimeSpan? Radius(this Interval<DateTime> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        public static DateTime? Centre(this Interval<DateTime> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        public static Interval<DateTime> Canonicalize(this Interval<DateTime> value, BoundaryType boundaryType, TimeSpan step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step)
            => IntervalHelper.ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step)
            => IntervalHelper.ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }
}
