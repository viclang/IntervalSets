using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<int> Length(this Interval<DateOnly> source)
            => ValueOrInfinity(source, (end, start) => end.DayNumber - start.DayNumber);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTime> source)
            => ValueOrInfinity(source, (end, start) => end - start);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> source)
            => ValueOrInfinity(source, (end, start) => end - start);

        [Pure]
        public static Infinity<double> Length(this Interval<double> source)
            => ValueOrInfinity(source, (end, start) => end - start);

        [Pure]
        public static Infinity<int> Length(this Interval<int> source)
            => ValueOrInfinity(source, (end, start) => end - start);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> source)
            => ValueOrInfinity(source, (end, start) => end - start);
    }
}
