using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static class IntervalExtensions
    {
        public static int Length(this Interval<int> interval)
        {
            var end = interval.End ?? int.MaxValue;
            var start = interval.Start ?? int.MinValue;
            return end - start;
        }

        public static TimeSpan Length(this Interval<DateTimeOffset> interval)
        {
            return (interval.End ?? DateTimeOffset.MaxValue).Subtract(interval.Start ?? DateTimeOffset.MinValue);
        }

        public static TimeSpan Length(this Interval<DateTime> interval)
        {
            return (interval.End ?? DateTime.MaxValue).Subtract(interval.Start ?? DateTime.MinValue);
        }

        public static int Length(this Interval<DateOnly> interval)
        {
            return (interval.End ?? DateOnly.MaxValue).DayNumber - (interval.Start ?? DateOnly.MinValue).DayNumber;
        }

        public static int CompareStart<T>(this Interval<T> value,
            Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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

        public static Interval<T> Hull<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
                min.StartInclusive,
                max.EndInclusive);
        }
    }
}
