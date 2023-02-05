using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static TimeSpan? Radius(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan()) / 2);

        [Pure]
        public static TimeOnly? Centre(this Interval<TimeOnly> value)
            => IntervalHelper.ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        [Pure]
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> value, BoundaryType boundaryType, TimeSpan step)
            => IntervalHelper.Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        [Pure]
        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> value, TimeSpan step)
            => IntervalHelper.ToClosed(value, x => x.Add(step), x => x.Add(-step));

        [Pure]
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> value, TimeSpan step)
            => IntervalHelper.ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }
}
