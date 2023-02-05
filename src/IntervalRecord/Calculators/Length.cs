using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<int> Length(this Interval<DateOnly> value)
            => ValueOrInfinity(value, (end, start) => end.DayNumber - start.DayNumber);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value)
            => ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value)
            => ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static Infinity<double> Length(this Interval<double> value)
            => ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static Infinity<int> Length(this Interval<int> value)
            => ValueOrInfinity(value, (end, start) => end - start);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value)
            => ValueOrInfinity(value, (end, start) => end - start);
    }
}
