using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static double? Radius(this Interval<DateOnly> source) => ValueOrNull(source, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> source) => ValueOrNull(source, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTimeOffset> source) => ValueOrNull(source, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Radius(this Interval<double> source) => ValueOrNull(source, (end, start) => (end - start) / 2);

        [Pure]
        public static double? Radius(this Interval<int> source) => ValueOrNull(source, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<TimeOnly> source)
            => ValueOrNull(source, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan()) / 2);
    }
}
