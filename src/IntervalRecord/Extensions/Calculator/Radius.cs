using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        
        public static double? Radius<T>(this Interval<T> value, Func<T, T, int> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : substract(value.End.Value, value.Start.Value)/2;

        public static double? Radius<T>(this Interval<T> value, Func<T, T, double> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : substract(value.End.Value, value.Start.Value) / 2;

        public static TimeSpan? Radius<T>(this Interval<T> value, Func<T, T, TimeSpan> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : substract(value.End.Value, value.Start.Value) / 2;
    }
}
