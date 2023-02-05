using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalExtensions
{
    public static class IntervalUtil
    {
        public static Interval<T> ToInclusive<T>(this T start, T end)
            where T : struct, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, true, true);
        }

        public static Interval<T> ToExclusive<T>(this T start, T end)
            where T : struct, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, true, false);
        }

        public static Interval<T> ToInfinity<T>(this T start)
            where T : struct, IComparable<T>, IComparable
        {
            return new Interval<T>(start, null, true, true);
        }

        public static Interval<T> FromInfinityToInclusive<T>(T end)
            where T : struct, IComparable<T>, IComparable
        {
            return new Interval<T>(null, end, true, true);
        }

        public static Interval<T> FromInfinityToExclusive<T>(T end)
            where T : struct, IComparable<T>, IComparable
        {
            return new Interval<T>(null, end, true, false);
        }

        public static int CompareStart<T>(this Interval<T> value,
            Interval<T> other)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.Start is null && other.Start is not null)
            {
                return -1;
            }

            if (value.Start is not null && other.Start is null)
            {
                return 1;
            }

            if (value.Start is null && other.Start is null)
            {
                return 0;
            }

            return value.Start!.Value.CompareTo(other.Start!.Value);
        }

        public static int CompareEnd<T>(this Interval<T> value,
            Interval<T> other)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.End is null && other.End is not null)
            {
                return 1;
            }

            if (value.End is not null && other.End is null)
            {
                return -1;
            }

            if (value.End is null && other.End is null)
            {
                return 0;
            }

            return value.End!.Value.CompareTo(other.End!.Value);
        }

        public static bool IsConnected(
            this Interval<DateOnly> value,
            Interval<DateOnly> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this Interval<DateTime> value,
            Interval<DateTime> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this Interval<DateTimeOffset> value,
            Interval<DateTimeOffset> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this Interval<int> value,
            Interval<int> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value + 1
                           : value.End.Value);
        }

        public static Interval<T> GetCollectionInterval<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IComparable<T>, IComparable
        {
            if (!values.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }

            var min = values.Any(x => x.Start is null) ? new Interval<T>()
                    : values.MinBy(x => x.Start);

            var max = values.Any(x => x.End is null) ? new Interval<T>()
                    : values.MaxBy(x => x.End!.Value);

            return new Interval<T>(
                min.Start,
                max.End,
                min.Inclusive.Start,
                max.Inclusive.End);
        }
    }
}
