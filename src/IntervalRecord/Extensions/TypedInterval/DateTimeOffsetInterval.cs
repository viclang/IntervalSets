using InfinityComparable;
using IntervalRecord.Internal;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        public static TimeSpan? Radius(this Interval<DateTimeOffset> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end - start) / 2);

        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> value, BoundaryType boundaryType, TimeSpan step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan step)
            => IntervalHelper.ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan step)
            => IntervalHelper.ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }
}
