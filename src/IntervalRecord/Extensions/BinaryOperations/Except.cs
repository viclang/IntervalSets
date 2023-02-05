using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Except<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.Overlaps(other, true))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(other),
                    "Intersection is only supported for connected intervals.");
            }
            var minByStart = MinBy(value, other, x => x.Start);
            var minByEnd = MinBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : minByEnd.EndInclusive;

            return value with { Start = minByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
