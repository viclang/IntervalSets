using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T> Hull<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(first, second, i => i.Start);
            var maxByEnd = MaxBy(first, second, i => i.End);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive || second.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive || second.EndInclusive
                : maxByEnd.EndInclusive;

            return new Interval<T>(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
        }

        [Pure]
        public static Interval<T>? Hull<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!source.Any())
            {
                return null;
            }
            var min = source.MinBy(i => i.Start);
            var max = source.MaxBy(i => i.End);

            return new Interval<T>(
                min.Start,
                max.End,
                min.StartInclusive,
                max.EndInclusive);
        }
    }
}
