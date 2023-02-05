using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T>? Gap<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.GetOverlappingState(second, true) switch
            {
                OverlappingState.Before => new Interval<T>(first.End, second.Start, !first.EndInclusive, !second.StartInclusive),
                OverlappingState.After => new Interval<T>(second.End, first.Start, !second.EndInclusive, !first.StartInclusive),
                _ => null
            };

        [Pure]
        public static Interval<T> GapOrDefault<T>(this Interval<T> first, Interval<T> second, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.GetOverlappingState(second, true) switch
            {
                OverlappingState.Before => new Interval<T>(first.End, second.Start, !first.EndInclusive, !second.StartInclusive),
                OverlappingState.After => new Interval<T>(second.End, first.Start, !second.EndInclusive, !first.StartInclusive),
                _ => defaultValue
            };

        [Pure]
        public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Gap(b));
    }
}
