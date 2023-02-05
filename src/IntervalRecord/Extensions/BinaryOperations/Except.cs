namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T>? Except<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.Overlaps(other, true))
            {
                return null;
            }
            return GetExceptValue(value, other);
        }

        public static Interval<T> ExceptOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.Overlaps(other, true))
            {
                return defaultValue;
            }
            return GetExceptValue(value, other);
        }

        private static Interval<T> GetExceptValue<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
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

        public static IEnumerable<Interval<T>> Except<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.PairwiseNotNullOrEmpty((a, b) => a.Except(b));
    }
}
