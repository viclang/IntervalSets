using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<T> Max<T>(Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => a >= b ? a : b;

        [Pure]
        public static Interval<T> MaxBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(a).CompareTo(selector(b)) >= 0 ? a : b;

        [Pure]
        public static Interval<T> Min<T>(Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => a <= b ? a : b;

        [Pure]
        public static Interval<T> MinBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(a).CompareTo(selector(b)) <= 0 ? a : b;

        private static Infinity<TResult> ValueOrInfinity<T, TResult>(Interval<T> source, Func<T, T, TResult> function)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
            => source.GetBoundedState() == BoundedState.Bounded
                        ? source.IsEmpty()
                            ? default
                            : function(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault())
                        : Infinity<TResult>.PositiveInfinity;

        private static TResult? ValueOrNull<T, TResult>(Interval<T> value, Func<T, T, TResult> function)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            return value.Start.IsInfinity || value.End.IsInfinity || value.IsEmpty()
                        ? null
                        : function(value.End.GetValueOrDefault(), value.Start.GetValueOrDefault());
        }
    }
}
