namespace IntervalRecord
{
    public static partial class Interval
    {
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

        public static IEnumerable<Interval<T>> Gaps<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.PairwiseNotNullOrEmpty((a, b) => a.Gap(b));
    }
}
