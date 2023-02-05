namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<int> Closure(this Interval<int> value, int step) => ToClosed(value, x => x + step, x => x - step);
        public static Interval<double> Closure(this Interval<double> value, double step) => ToClosed(value, x => x + step, x => x - step);
        public static Interval<DateOnly> Closure(this Interval<DateOnly> value, int step) => ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> value, TimeSpan step) => ToClosed(value, x => x.Add(step), x => x.Add(-step));
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step) => ToClosed(value, x => x + step, x => x - step);
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan step) => ToClosed(value, x => x + step, x => x - step);
    }
}
