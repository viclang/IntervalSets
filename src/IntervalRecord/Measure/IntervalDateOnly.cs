using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Infinity<int> Length(this Interval<DateOnly> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded)
            {
                return Infinity<int>.PositiveInfinity;
            }
            if (source.IsEmpty())
            {
                return 0;
            }
            else
            {
                return source.End.GetValueOrDefault().DayNumber - source.Start.GetValueOrDefault().DayNumber;
            }
        }

        [Pure]
        public static DateOnly? Centre(this Interval<DateOnly> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return source.Start.GetValueOrDefault()
                .AddDays((source.End.GetValueOrDefault().DayNumber - source.Start.GetValueOrDefault().DayNumber) / 2);
        }

        [Pure]
        public static double? Radius(this Interval<DateOnly> source)
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return ((double)source.End.GetValueOrDefault().DayNumber - source.Start.GetValueOrDefault().DayNumber) / 2;
        }
    }
}
