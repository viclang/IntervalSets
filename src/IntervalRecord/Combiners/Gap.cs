using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Gap<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.GetOverlappingState(other, true) switch
            {
                OverlappingState.Before => new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive),
                OverlappingState.After => new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive),
                _ => null
            };

        [Pure]
        public static Interval<T> GapOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.GetOverlappingState(other, true) switch
            {
                OverlappingState.Before => new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive),
                OverlappingState.After => new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive),
                _ => defaultValue
            };

        [Pure]
        public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Gap(b));
    }
}
