using InfinityComparable;
using System;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Except<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IComparable<T>, IComparable
        {
            if (!value.Overlaps(other, true))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(other),
                    "Except is only supported for connected intervals.");
            }
            var minByStart = MinBy(value, other, x => x.Start);
            var maxByStart = MaxBy(value, other, x => x.Start);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByStart.EndInclusive;

            return value with { Start = minByStart.Start, End = maxByStart.Start, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
