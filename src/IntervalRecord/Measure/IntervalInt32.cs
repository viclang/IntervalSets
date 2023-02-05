using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<int> Length(this Interval<int> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded)
            {
                return Infinity<int>.PositiveInfinity;
            }
            if (source.IsEmpty())
            {
                return 0;
            }
            return source.End.GetValueOrDefault() - source.Start.GetValueOrDefault();
        }

        [Pure]
        public static double? Centre(this Interval<int> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return ((double)source.End.GetValueOrDefault() + source.Start.GetValueOrDefault()) / 2;
        }

        [Pure]
        public static double? Radius(this Interval<int> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return ((double)source.End.GetValueOrDefault() - source.Start.GetValueOrDefault()) / 2;
        }
    }
}
