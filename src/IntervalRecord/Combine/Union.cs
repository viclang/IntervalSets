using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Union<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? null
                : Hull(first, second);

        [Pure]
        public static Interval<T> UnionOrDefault<T>(this Interval<T> first, Interval<T> second, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? defaultValue
                : Hull(first, second);

        [Pure]
        public static IEnumerable<Interval<T>> Union<T>(this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Reduce((a, b) => a.Union(b));
    }
}
