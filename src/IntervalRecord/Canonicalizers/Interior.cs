using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<DateOnly> Interior(this Interval<DateOnly> source, int step)
            => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));

        [Pure]
        public static Interval<DateTime> Interior(this Interval<DateTime> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Add(-step));

        [Pure]
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Add(-step));

        [Pure]
        public static Interval<double> Interior(this Interval<double> source, double step)
            => ToOpen(source, end => end + step, start => start - step);

        [Pure]
        public static Interval<int> Interior(this Interval<int> source, int step)
            => ToOpen(source, end => end + step, start => start - step);

        [Pure]
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Add(-step));
    }
}
