using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T> Empty<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(default(T), default(T), false, false);

        [Pure]
        public static Interval<T> All<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new();

        [Pure]
        public static Interval<T> Singleton<T>(T value) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(value, value, true, true);

        [Pure]
        public static Interval<T> WithIntervalType<T>(Infinity<T> start, Infinity<T> end, IntervalType intervalType) where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var (startInclusive, endInclusive) = intervalType.ToTuple();
            return new(start, end, startInclusive, endInclusive);
        }

        [Pure]
        public static Interval<T> Open<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, false, false);

        [Pure]
        public static Interval<T> Closed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, true, true);

        [Pure]
        public static Interval<T> OpenClosed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, false, true);

        [Pure]
        public static Interval<T> ClosedOpen<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, true, false);

        [Pure]
        public static Interval<T> GreaterThan<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, Infinity<T>.PositiveInfinity, false, false);

        [Pure]
        public static Interval<T> AtLeast<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, Infinity<T>.PositiveInfinity, true, false);

        [Pure]
        public static Interval<T> LessThan<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(Infinity<T>.NegativeInfinity, end, false, false);

        [Pure]
        public static Interval<T> AtMost<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(Infinity<T>.NegativeInfinity, end, false, true);
    }
}
