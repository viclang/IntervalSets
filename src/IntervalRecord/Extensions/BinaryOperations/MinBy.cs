namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> MinBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(a).CompareTo(selector(b)) == -1
                ? a
                : b;
    }
}
