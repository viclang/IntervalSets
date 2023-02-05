using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public enum InfinityState
    {
        Bounded = 0,
        Unbounded = 1,
        LeftBounded = 2,
        RightBounded = 3,
    }

    public static partial class Interval
    {
        [Pure]
        public static InfinityState GetInfinityState<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (value.Start.IsInfinite, value.End.IsInfinite) switch
        {
            (false, false) => InfinityState.Bounded,
            (true, true) => InfinityState.Unbounded,
            (true, false) => InfinityState.RightBounded,
            (false, true) => InfinityState.LeftBounded
        };

        [Pure]
        public static bool IsHalfBounded<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite && !value.End.IsInfinite || !value.Start.IsInfinite && value.End.IsInfinite;
    }
}
