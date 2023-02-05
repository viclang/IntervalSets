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
            => new(start, null, false, false);

        [Pure]
        public static Interval<T> AtLeast<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, null, true, false);

        [Pure]
        public static Interval<T> LessThan<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(null, end, false, false);

        [Pure]
        public static Interval<T> AtMost<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(null, end, false, true);
    }
}
