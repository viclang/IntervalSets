namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Hull<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(value, other, x => x.Start);
            var maxByEnd = MaxBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByEnd.EndInclusive;

            return new Interval<T>(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
        }
    }
}
