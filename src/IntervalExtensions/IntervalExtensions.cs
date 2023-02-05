using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalExtensions.Comparers;
using IntervalExtensions.Interfaces;

namespace IntervalExtensions
{
    public static class IntervalExtensions
    {
        public static int Compare<T>(this IInterval<T> value,
            IInterval<T> other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.Start is not null
                && other.End is not null
                && value.Start.Value.IsGreaterThan(other.End.Value, hasInclusiveEnd))
            {
                return 1;
            }

            if (value.End is not null
                && other.Start is not null
                && value.End.Value.IsSmallerThan(other.Start.Value, hasInclusiveEnd))
            {
                return -1;
            }

            return 0;
        }

        public static int CompareStart<T>(this IInterval<T> value,
            IInterval<T> other)
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

        public static int CompareEnd<T>(this IInterval<T> value,
            IInterval<T> other)
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

        public static bool IsGreaterThan<T>(this T value, T other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return hasInclusiveEnd
                ? value.CompareTo(other) is 1
                : value.CompareTo(other) is 0 or 1;
        }

        public static bool IsSmallerThan<T>(this T value, T other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return hasInclusiveEnd
                ? value.CompareTo(other) is -1
                : value.CompareTo(other) is -1 or 0;
        }

        public static bool IsConnected(
            this IInterval<DateOnly> value,
            IInterval<DateOnly> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this IInterval<DateTime> value,
            IInterval<DateTime> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this IInterval<DateTimeOffset> value,
            IInterval<DateTimeOffset> other,
            bool hasInclusiveEnd)
        {

            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value.AddDays(1)
                           : value.End.Value);
        }

        public static bool IsConnected(
            this IInterval<int> value,
            IInterval<int> other,
            bool hasInclusiveEnd)
        {
            return value.End is not null
                   && other.Start.Equals(
                       hasInclusiveEnd
                           ? value.End.Value + 1
                           : value.End.Value);
        }

        public static bool IsValidInterval<T>(
            this IInterval<T> value, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return value.End is null
                   || value.End is not null
                   && (value.Start is null 
                       || value.Start is not null
                       && value.End!.Value.IsGreaterThan(value.Start!.Value, hasInclusiveEnd));
        }

        private static Interval<T> GetCollectionInterval<T>(
            this IEnumerable<IInterval<T>> values)
            where T : struct, IComparable<T>, IComparable
        {
            if (!values.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }

            return new Interval<T>(
                values.Any(x => x.Start is null) ? null
                    : values.Min(x => x.Start),
                values.Any(x => x.End is null) ? null
                    : values.Max(x => x.End!.Value));
        }
        
        public static IInterval<T>? GetPreviousInterval<T>(
            this IInterval<T> value,
            IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable<T?>, IComparable
        {
            return intervals
                .Where(x => x.CompareStart(value) is -1)
                .MaxBy(x => x.Start);
        }

        public static IInterval<T>? GetNextInterval<T>(this IInterval<T> value, IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable
        {
            return intervals
                .Where(x => x.CompareStart(value) is 1)
                .MinBy(x => x.Start);
        }

        public static IInterval<T>? GetFirstInterval<T>(this IEnumerable<IInterval<T>> values)
            where T : struct, IComparable<T>, IComparable
        {
            return  values.FirstOrDefault(x => x.Start is null) ?? values.MinBy(x => x.Start!.Value);
        }

        public static IInterval<T>? GetLastInterval<T>(this IEnumerable<IInterval<T>> values)
            where T : struct, IComparable<T>, IComparable
        {
            return values.LastOrDefault(x => x.End is null) ?? values.MaxBy(x => x.End!.Value);
        }

        public static bool OverlapsWith<T>(this IInterval<T> value, IInterval<T> other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return Compare(value, other, hasInclusiveEnd) is 0;
        }

        public static bool IsBefore<T>(this IInterval<T> value, IInterval<T> other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return Compare(value, other, hasInclusiveEnd) is -1;
        }

        public static bool IsAfter<T>(this IInterval<T> value, IInterval<T> other, bool hasInclusiveEnd)
            where T : struct, IComparable<T>, IComparable
        {
            return Compare(value, other, hasInclusiveEnd) is 1;
        }
    }
}
