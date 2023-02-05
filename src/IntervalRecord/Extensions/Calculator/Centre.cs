using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static double? Centre<T>(this Interval<T> value, Func<T, T, int> add)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : add(value.Start.Value, value.End.Value) / 2;
        public static double? Centre<T>(this Interval<T> value, Func<T, T, double> add)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : add(value.Start.Value, value.End.Value) / 2;
        public static T? Centre<T>(this Interval<T> value, Func<T, TimeSpan, T> add, Func<T, T, TimeSpan> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : add(value.Start.Value, substract(value.End.Value, value.Start.Value) / 2);
    }
}
