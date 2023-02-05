using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Intersect<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? null
                : GetIntersectValue(value, other);

        [Pure]
        public static Interval<T> IntersectOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? defaultValue
                : GetIntersectValue(value, other);

        [Pure]
        public static IEnumerable<Interval<T>> Intersect<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Intersect(b), x => !x.IsEmpty());

        [Pure]
        private static Interval<T> GetIntersectValue<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var maxByStart = MaxBy(value, other, x => x.Start);
            var minByEnd = MinBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive && other.StartInclusive
                : maxByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive && other.EndInclusive
                : minByEnd.EndInclusive;

            return value with { Start = maxByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
