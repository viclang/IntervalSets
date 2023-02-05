using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<int> Length(this Interval<int> source) => Length(source, (left, right) => left - right);

        [Pure]
        public static Infinity<double> Length(this Interval<double> source) => Length(source, (left, right) => left - right);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTime> source) => Length(source, (left, right) => left - right);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> source) => Length(source, (left, right) => left - right);

        [Pure]
        public static Infinity<int> Length(this Interval<DateOnly> source) => Length(source, (left, right) => left.DayNumber - right.DayNumber);

        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<TimeOnly> source) => Length(source, (left, right) => left - right);

        [Pure]
        private static Infinity<TResult> Length<T, TResult>(Interval<T> source, Func<T, T, TResult> length)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            if (source.GetBoundedState() != BoundedState.Bounded)
            {
                return Infinity<TResult>.PositiveInfinity;
            }
            if (source.IsEmpty())
            {
                return default(TResult);
            }
            return length(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
        }
    }
}
