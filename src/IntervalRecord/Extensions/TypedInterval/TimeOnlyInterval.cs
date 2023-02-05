using InfinityComparable;
using IntervalRecord.Internal;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        public static TimeSpan? Radius(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan()) / 2);

        public static TimeOnly? Centre(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> value, BoundaryType boundaryType, TimeSpan step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> value, TimeSpan step)
            => IntervalHelper.ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> value, TimeSpan step)
            => IntervalHelper.ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }
}
