using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Except<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true) ? null : GetExceptValue(first, second);

        [Pure]
        public static Interval<T> ExceptOrDefault<T>(this Interval<T> first, Interval<T> second, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true) ? defaultValue : GetExceptValue(first, second);

        [Pure]
        public static IEnumerable<Interval<T>> Except<T>(this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Except(b)).Where(i => !i.IsEmpty());

        [Pure]
        private static Interval<T> GetExceptValue<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(first, second, i => i.Start);
            var maxByStart = MaxBy(first, second, i => i.Start);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive || second.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive || second.EndInclusive
                : maxByStart.EndInclusive;

            return first with { Start = minByStart.Start, End = maxByStart.Start, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
