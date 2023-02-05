using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Radius(this Interval<int> value, int step) => Radius(Closure(value, step), (a, b) => (a - b) / 2);
        public static double? Radius(this Interval<double> value, double step) => Radius(Closure(value, step), (a, b) => (a - b) / 2);
        public static double? Radius(this Interval<DateOnly> value, int step) => Radius(Closure(value, step), (a, b) => (a.DayNumber - b.DayNumber) / 2);
        public static TimeSpan? Radius(this Interval<TimeOnly> value, TimeSpan step) => Radius(Closure(value, step), (a, b) => (a.ToTimeSpan() - b.ToTimeSpan()) / 2);
        public static TimeSpan? Radius(this Interval<DateTime> value, TimeSpan step) => Radius(Closure(value, step), (a, b) => a.Subtract(b) / 2);
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value, TimeSpan step) => Radius(Closure(value, step), (a, b) => a.Subtract(b) / 2);

        public static double? Radius(this Interval<int> value) => Radius(value, (a, b) => (a - b)/2);
        public static double? Radius(this Interval<double> value) => Radius(value, (a, b) => (a - b)/2);
        public static double? Radius(this Interval<DateOnly> value) => Radius(value, (a, b) => (a.DayNumber - b.DayNumber)/2);
        public static TimeSpan? Radius(this Interval<TimeOnly> value) => Radius(value, (a, b) => (a.ToTimeSpan() - b.ToTimeSpan())/2);
        public static TimeSpan? Radius(this Interval<DateTime> value) => Radius(value, (a, b) => a.Subtract(b)/2);
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value) => Radius(value, (a, b) => a.Subtract(b)/2);

        private static TResult? Radius<T, TResult>(this Interval<T> value, Func<T, T, TResult> radius)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : radius(value.End.Value, value.Start.Value);
    }
}
