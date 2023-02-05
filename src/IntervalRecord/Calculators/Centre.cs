using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static DateOnly? Centre(this Interval<DateOnly> value)
            => ValueOrNull(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber) / 2));

        [Pure]
        public static DateTime? Centre(this Interval<DateTime> value)
            => ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        [Pure]
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value)
            => ValueOrNull(value, (end, start) => start.Add((end - start) / 2));

        [Pure]
        public static double? Centre(this Interval<double> value)
            => ValueOrNull(value, (end, start) => (end + start) / 2);

        [Pure]
        public static double? Centre(this Interval<int> value)
            => ValueOrNull(value, (end, start) => (end + (double)start) / 2);

        [Pure]
        public static TimeOnly? Centre(this Interval<TimeOnly> value)
            => ValueOrNull(value, (end, start) => start.Add((end - start) / 2));
    }
}
