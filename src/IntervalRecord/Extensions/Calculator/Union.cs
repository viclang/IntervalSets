using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Union<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.IsConnected(other, true))
            {
                throw new ArgumentOutOfRangeException("other", "Union is not continuous.");
            }
            return Hull(value, other);
        }
    }
}
