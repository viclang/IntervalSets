using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static double? Radius(this Interval<DateOnly> value)
            => ValueOrNull(value, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> value)
            => ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value)
            => ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Radius(this Interval<double> value)
            => ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Radius(this Interval<int> value)
            => ValueOrNull(value, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<TimeOnly> value)
            => ValueOrNull(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan()) / 2);
    }
}
