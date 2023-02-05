using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Intersect<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? null
                : GetIntersectValue(first, second);

        [Pure]
        public static Interval<T> IntersectOrDefault<T>(this Interval<T> first, Interval<T> second, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? defaultValue
                : GetIntersectValue(first, second);

        [Pure]
        public static IEnumerable<Interval<T>> Intersect<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Intersect(b)).Where(i => !i.IsEmpty());

        [Pure]
        private static Interval<T> GetIntersectValue<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var maxByStart = MaxBy(first, second, i => i.Start);
            var minByEnd = MinBy(first, second, i => i.End);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive && second.StartInclusive
                : maxByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive && second.EndInclusive
                : minByEnd.EndInclusive;

            return first with { Start = maxByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
