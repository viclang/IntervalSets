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
            => selector(a).CompareTo(selector(b)) == 1
                ? a
                : b;
    }
}
