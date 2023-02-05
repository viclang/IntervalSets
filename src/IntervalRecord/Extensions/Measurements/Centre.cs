namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Centre(this Interval<int> value, int closureStep) => Centre(Closure(value, closureStep));
        public static double? Centre(this Interval<double> value, double closureStep) => Centre(Closure(value, closureStep));
        public static DateOnly? Centre(this Interval<DateOnly> value, int closureStep) => Centre(Closure(value, closureStep));
        public static TimeOnly? Centre(this Interval<TimeOnly> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));
        public static DateTime? Centre(this Interval<DateTime> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value, TimeSpan closureStep) => Centre(Closure(value, closureStep));

        public static double? Centre(this Interval<int> value) => Centre(value, (end, start) => (end + (double)start)/2);
        public static double? Centre(this Interval<double> value) => Centre(value, (end, start) => (end + start)/2);
        public static DateOnly? Centre(this Interval<DateOnly> value) => Centre(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber)/2));
        public static TimeOnly? Centre(this Interval<TimeOnly> value) => Centre(value, (end, start) => start.Add((end - start)/2));
        public static DateTime? Centre(this Interval<DateTime> value) => Centre(value, (end, start) => start.Add((end - start)/2));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value) => Centre(value, (end, start) => start.Add((end - start) / 2));

        private static TResult? Centre<T, TResult>(this Interval<T> value, Func<T, T, TResult> centre)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : centre(value.End.Finite.Value, value.Start.Finite.Value);
    }
}
