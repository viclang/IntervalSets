using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<TimeSpan> Length(this Interval<DateTime> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded)
            {
                return Infinity<TimeSpan>.PositiveInfinity;
            }
            if (source.IsEmpty())
            {
                return TimeSpan.Zero;
            }
            else
            {
                return source.End.GetValueOrDefault() - source.Start.GetValueOrDefault();
            }
        }

        [Pure]
        public static DateTime? Centre(this Interval<DateTime> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return source.Start.GetValueOrDefault().Add((source.End.GetValueOrDefault() - source.Start.GetValueOrDefault()) / 2);
        }

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return (source.End.GetValueOrDefault() - source.Start.GetValueOrDefault()) / 2;
        }
    }
}
