using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Infinity<int> Length(this Interval<int> value, int closureStep) => Length(Closure(value, closureStep));
        public static Infinity<double> Length(this Interval<double> value, double closureStep) => Length(Closure(value, closureStep));
        public static Infinity<int> Length(this Interval<DateOnly> value, int closureStep) => Length(Closure(value, closureStep));
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value, TimeSpan closureStep) => Length(Closure(value, closureStep));
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value, TimeSpan closureStep) => Length(Closure(value, closureStep));
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value, TimeSpan closureStep) => Length(Closure(value, closureStep));

        public static Infinity<int> Length(this Interval<int> value) => Length(value, (a, b) => a - b);
        public static Infinity<double> Length(this Interval<double> value) => Length(value, (a, b) => a - b);
        public static Infinity<int> Length(this Interval<DateOnly> value) => Length(value, (a, b) => a.DayNumber - b.DayNumber);
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> value) => Length(value, (a, b) => a.ToTimeSpan() - a.ToTimeSpan());
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value) => Length(value, (a, b) => a.Subtract(b));
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value) => Length(value, (a, b) => a.Subtract(b));

        private static Infinity<TResult> Length<T, TResult>(this Interval<T> value, Func<T, T, TResult> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<TResult>()
                : value.IsEmpty() ? default : substract(value.End.Value, value.Start.Value);
    }
}
