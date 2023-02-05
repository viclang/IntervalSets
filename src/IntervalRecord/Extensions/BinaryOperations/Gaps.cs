using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Gap<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsBefore(other))
            {
                return new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive);
            }
            if (value.IsAfter(other))
            {
                return new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive);
            }
            return null;
        }

        [Pure]
        public static Interval<T> GapOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsBefore(other))
            {
                return new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive);
            }
            if (value.IsAfter(other))
            {
                return new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive);
            }
            return defaultValue;
        }

        [Pure]
        public static IEnumerable<Interval<T>> Complement<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.PairwiseFilter((a, b) => a.Gap(b), x => !x.IsEmpty());
    }
}
