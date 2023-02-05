using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Centre(this Interval<int> value, int closureStep) => Centre(Closure(value, closureStep));
        public static double? Centre(this Interval<double> value, double closureStep) => Centre(Closure(value, closureStep));
        public static double? Centre(this Interval<DateOnly> value, int closureStep) => Centre(Closure(value, closureStep));
        public static TimeOnly? Centre(this Interval<TimeOnly> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));
        public static DateTime? Centre(this Interval<DateTime> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));

        public static double? Centre(this Interval<int> value) => Centre(value, (a, b) => (a + (double)b)/2);
        public static double? Centre(this Interval<double> value) => Centre(value, (a, b) => (a + b)/2);
        public static double? Centre(this Interval<DateOnly> value) => Centre(value, (a, b) => (a.DayNumber + (double)b.DayNumber)/2);
        public static TimeOnly? Centre(this Interval<TimeOnly> value) => Centre(value, (a, b) => a.Add((b.ToTimeSpan() - a.ToTimeSpan())/2));
        public static DateTime? Centre(this Interval<DateTime> value) => Centre(value, (a, b) => a.Add(b.Subtract(a)/2));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value) => Centre(value, (a, b) => b.Add(a.Subtract(b) / 2));

        private static TResult? Centre<T, TResult>(this Interval<T> value, Func<T, T, TResult> radius)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : radius(value.End.Value, value.Start.Value);
    }
}
