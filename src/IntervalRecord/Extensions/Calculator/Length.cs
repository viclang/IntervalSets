using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {        
        public static Infinity<int> Length<T>(this Interval<T> value, Func<T, T, int> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<int>()
                : value.IsEmpty() ? 0 : substract(value.End.Value, value.Start.Value);

        public static Infinity<double> Length<T>(this Interval<T> value, Func<T, T, double> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<double>()
                : value.IsEmpty() ? 0 : substract(value.End.Value, value.Start.Value);

        public static Infinity<TimeSpan> Length<T>(this Interval<T> value, Func<T, T, TimeSpan> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<TimeSpan>()
                : value.IsEmpty() ? TimeSpan.Zero : substract(value.End.Value, value.Start.Value);
    }
}
