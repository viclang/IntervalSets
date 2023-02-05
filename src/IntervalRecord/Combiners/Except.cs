using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Except<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true) ? null : GetExceptValue(value, other);

        [Pure]
        public static Interval<T> ExceptOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true) ? defaultValue : GetExceptValue(value, other);

        [Pure]
        public static IEnumerable<Interval<T>> Except<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Except(b)).Where(x => !x.IsEmpty());

        [Pure]
        private static Interval<T> GetExceptValue<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(value, other, x => x.Start);
            var maxByStart = MaxBy(value, other, x => x.Start);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByStart.EndInclusive;

            return value with { Start = minByStart.Start, End = maxByStart.Start, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
