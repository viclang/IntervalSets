using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static bool HasGap(this Interval<int> value, Interval<int> other, int step) => DistanceTo(value.Closure(step), other) > step;

        public static Interval<T>? Gap<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsBefore(other))
            {
                var gap = new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive);
                return gap.IsEmpty() ? null : gap;
            }
            if (value.IsAfter(other))
            {
                var gap = new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive);
                return gap.IsEmpty() ? null : gap;
            }
            return null;
        }

        /// <summary>
        /// Order does matter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int DistanceTo<T>(this Interval<T> value, Interval<T> other, Func<T, T, int> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.IsConnected(other)
                ? 0
                : substract(other.Start.Value, value.End.Value);
    }
}
