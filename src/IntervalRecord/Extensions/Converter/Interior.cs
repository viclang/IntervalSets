namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<int> Interior(this Interval<int> value, int step) => ToOpen(value, x => x + step, x => x - step);
        public static Interval<double> Interior(this Interval<double> value, double step) => ToOpen(value, x => x + step, x => x - step);
        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, int step) => ToOpen(value, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> value, TimeSpan step) => ToOpen(value, x => x.Add(step), x => x.Add(-step));
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step) => ToOpen(value, x => x + step, x => x - step);
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan step) => ToOpen(value, x => x + step, x => x - step);
    }
}
