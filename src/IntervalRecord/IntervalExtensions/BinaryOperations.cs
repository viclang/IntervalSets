using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
        [Pure]
        public static Interval<T>? Union<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? null
                : Hull(value, other);

        [Pure]
        public static Interval<T> UnionOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? defaultValue
                : Hull(value, other);

        [Pure]
        public static Interval<T> Hull<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = Interval.MinBy(value, other, x => x.Start);
            var maxByEnd = Interval.MaxBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByEnd.EndInclusive;

            return new Interval<T>(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
        }

        [Pure]
        public static Interval<T>? Intersect<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? null
                : GetIntersectValue(value, other);

        [Pure]
        public static Interval<T> IntersectOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !value.Overlaps(other, true)
                ? defaultValue
                : GetIntersectValue(value, other);

        [Pure]
        private static Interval<T> GetIntersectValue<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var maxByStart = Interval.MaxBy(value, other, x => x.Start);
            var minByEnd = Interval.MinBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive && other.StartInclusive
                : maxByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive && other.EndInclusive
                : minByEnd.EndInclusive;

            return value with { Start = maxByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }

        [Pure]
        public static Interval<T>? Except<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return !value.Overlaps(other, true) ? null : GetExceptValue(value, other);
        }

        [Pure]
        public static Interval<T> ExceptOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return !value.Overlaps(other, true) ? defaultValue : GetExceptValue(value, other);
        }

        [Pure]
        private static Interval<T> GetExceptValue<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = Interval.MinBy(value, other, x => x.Start);
            var maxByStart = Interval.MaxBy(value, other, x => x.Start);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByStart.EndInclusive;

            return value with { Start = minByStart.Start, End = maxByStart.Start, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }

        [Pure]
        public static Interval<T>? Gap<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsBefore(other))
            {
                return new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive);
            }
            if (value.IsAfter(other))
            {
                return new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive);
            }
            return null;
        }

        [Pure]
        public static Interval<T> GapOrDefault<T>(this Interval<T> value, Interval<T> other, Interval<T> defaultValue)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsBefore(other))
            {
                return new Interval<T>(value.End, other.Start, !value.EndInclusive, !other.StartInclusive);
            }
            if (value.IsAfter(other))
            {
                return new Interval<T>(other.End, value.Start, !other.EndInclusive, !value.StartInclusive);
            }
            return defaultValue;
        }
    }
}
