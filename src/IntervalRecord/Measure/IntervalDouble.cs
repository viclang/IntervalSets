using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<double> Length(this Interval<double> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded)
            {
                return Infinity<double>.PositiveInfinity;
            }
            if (source.IsEmpty())
            {
                return 0;
            }
            return (double)(source.End.GetValueOrDefault() - source.Start.GetValueOrDefault());
        }

        [Pure]
        public static double? Centre(this Interval<double> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return ((double)source.End.GetValueOrDefault() + source.Start.GetValueOrDefault()) / 2;
        }

        [Pure]
        public static double? Radius(this Interval<double> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return ((double)source.End.GetValueOrDefault() - source.Start.GetValueOrDefault()) / 2;
        }
    }
}
