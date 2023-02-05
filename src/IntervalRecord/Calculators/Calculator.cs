using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        private static Infinity<TResult> ValueOrInfinity<T, TResult>(Interval<T> value, Func<T, T, TResult> function)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            return value.Start.IsInfinite || value.End.IsInfinite
                        ? Infinity<TResult>.PositiveInfinity
                        : value.IsEmpty() ? default : function(value.End.Finite.Value, value.Start.Finite.Value);
        }

        [Pure]
        private static TResult? ValueOrNull<T, TResult>(Interval<T> value, Func<T, T, TResult> function)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            return value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                        ? null
                        : function(value.End.Finite.Value, value.Start.Finite.Value);
        }
    }
}
