namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T>? Union<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? null
                : Hull(value, other);

        public static Interval<T> UnionOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? defaultValue
                : Hull(value, other);

        public static IEnumerable<Interval<T>> Union<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.UnionOrDefault(b, a));
    }
}
