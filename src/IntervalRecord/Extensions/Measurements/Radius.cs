namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Radius(this Interval<int> value, int closureStep) => Radius(Closure(value, closureStep));
        public static double? Radius(this Interval<double> value, double closureStep) => Radius(Closure(value, closureStep));
        public static double? Radius(this Interval<DateOnly> value, int closureStep) => Radius(Closure(value, closureStep));
        public static TimeSpan? Radius(this Interval<TimeOnly> value, TimeSpan closureStep) => Radius(Closure(value, closureStep));
        public static TimeSpan? Radius(this Interval<DateTime> value, TimeSpan closureStep) => Radius(Closure(value, closureStep));
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value, TimeSpan closureStep) => Radius(Closure(value, closureStep));

        public static double? Radius(this Interval<int> value) => Radius(value, (end, start) => (end - start)/2);
        public static double? Radius(this Interval<double> value) => Radius(value, (end, start) => (end - start)/2);
        public static double? Radius(this Interval<DateOnly> value) => Radius(value, (end, start) => (end.DayNumber - start.DayNumber)/2);
        public static TimeSpan? Radius(this Interval<TimeOnly> value) => Radius(value, (end, start) => (end.ToTimeSpan() - start.ToTimeSpan())/2);
        public static TimeSpan? Radius(this Interval<DateTime> value) => Radius(value, (end, start) => end.Subtract(start)/2);
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value) => Radius(value, (end, start) => end.Subtract(start)/2);

        private static TResult? Radius<T, TResult>(this Interval<T> value, Func<T, T, TResult> radius)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : radius(value.End.Finite.Value, value.Start.Finite.Value);
    }
}
