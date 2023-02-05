using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public enum BoundedState : byte
    {
        Bounded = 0,
        Unbounded = 1,
        LeftBounded = 2,
        RightBounded = 3,
    }

    public static partial class Interval
    {
        [Pure]
        public static BoundedState GetBoundedState<T>(this Interval<T> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (source.Start.State, source.End.State) switch
            {
                (InfinityState.IsFinite, InfinityState.IsFinite) => BoundedState.Bounded,
                (InfinityState.IsInfinity, InfinityState.IsInfinity) => BoundedState.Unbounded,
                (InfinityState.IsInfinity, InfinityState.IsFinite) => BoundedState.RightBounded,
                (InfinityState.IsFinite, InfinityState.IsInfinity) => BoundedState.LeftBounded,
                _ => throw new NotSupportedException()
            };

        [Pure]
        public static bool IsHalfBounded<T>(this Interval<T> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.GetBoundedState() is BoundedState.LeftBounded or BoundedState.RightBounded;
    }
}
