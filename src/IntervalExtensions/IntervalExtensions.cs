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
        public static bool IsGreaterThan<T>(this T x, T y, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return isInclusive
                ? x.CompareEnd(y) is 1
                : x.CompareEnd(y) is 0 or 1;
        }

        public static bool IsSmallerThan<T>(this T x, T y, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return isInclusive
                ? x.CompareEnd(y) is -1
                : x.CompareEnd(y) is -1 or 0;
        }

        public static bool IsConnected(
            this IInterval<DateOnly> source,
            IInterval<DateOnly> value,
            bool isInclusive)
        {
            return source.End is not null
                   && value.Start.Equals(
                       isInclusive
                           ? source.End.Value.AddDays(1)
                           : source.End.Value);
        }

        public static bool IsConnected(
            this IInterval<DateTime> source,
            IInterval<DateTime> value,
            bool isInclusive)
        {
            return source.End is not null
                   && value.Start.Equals(
                       isInclusive
                           ? source.End.Value.AddDays(1)
                           : source.End.Value);
        }

        public static bool IsConnected(
            this IInterval<DateTimeOffset> source,
            IInterval<DateTimeOffset> value,
            bool isInclusive)
        {

            return source.End is not null
                   && value.Start.Equals(
                       isInclusive
                           ? source.End.Value.AddDays(1)
                           : source.End.Value);
        }

        public static bool IsConnected(
            this IInterval<int> source,
            IInterval<int> value,
            bool isInclusive)
        {
            return source.End is not null
                   && value.Start.Equals(
                       isInclusive
                           ? source.End.Value + 1
                           : source.End.Value);
        }

        public static bool IsValidInterval<T>(
            this IInterval<T> value, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return value.End is null 
                   || value.End is not null
                   && value.End!.Value.IsGreaterThan(value.Start, isInclusive);
        }

        private static Interval<T> GetCollectionInterval<T>(
            this IEnumerable<IInterval<T>> values)
            where T : struct, IComparable<T>, IComparable
        {
            if (!values.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }

            return new Interval<T>(values.Min(x => x.Start),
                values.Any(x => x.End is null) ? null
                    : values.Max(x => x.End!.Value));
        }
        
        public static IInterval<T>? GetPreviousInterval<T>(
            this IInterval<T> value,
            IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable
        {
            return intervals
                .Where(x => x.Start.CompareEnd(value.Start) is -1)
                .MaxBy(x => x.Start);
        }

        public static IInterval<T>? GetNextInterval<T>(this IInterval<T> value, IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable
        {
            return intervals
                .Where(x => x.Start.CompareEnd(value.Start) is 1)
                .MinBy(x => x.Start);
        }

        public static IInterval<T>? GetFirstInterval<T>(this IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable
        {
            return intervals.MinBy(x => x.Start);
        }

        public static IInterval<T>? GetLastInterval<T>(this IEnumerable<IInterval<T>> intervals)
            where T : struct, IComparable<T>, IComparable
        {
            return intervals.MaxBy(x => x.Start);
        }

        public static bool OverlapsWith<T>(this IInterval<T> source, IInterval<T> value, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return new OverlapComparer<T>(isInclusive).Compare(source, value) is 0;
        }

        public static bool IsBefore<T>(this IInterval<T> source, IInterval<T> value, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return new OverlapComparer<T>(isInclusive).Compare(source, value) is -1;
        }

        public static bool IsAfter<T>(this IInterval<T> source, IInterval<T> value, bool isInclusive)
            where T : struct, IComparable<T>, IComparable
        {
            return new OverlapComparer<T>(isInclusive).Compare(source, value) is 1;
        }
    }
}
