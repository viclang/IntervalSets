using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Extensions
{
    public static partial class Interval
    {
        public static int? Length(this Interval<DateOnly> value)
        {
            if (!value.IsBounded())
            {
                return null;
            }

            if (value.IsEmpty())
            {
                return 0;
            }

            var (start, end) = value.ToNonNullableTuple();
            return end.DayNumber - start.DayNumber;
        }

        public static double? Radius(this Interval<DateOnly> value)
        {
            if (!value.IsBounded())
            {
                return null;
            }

            if (value.IsEmpty())
            {
                return 0;
            }

            return value.Length() / 2;
        }

        public static double? Centre(this Interval<DateOnly> value)
        {
            if (!value.IsBounded())
            {
                return null;
            }

            if (value.IsEmpty())
            {
                return 0;
            }

            var start = value.Start ?? DateOnly.MinValue;
            var end = value.End ?? DateOnly.MaxValue;
            return (start.DayNumber + end.DayNumber) / 2;
        }

        public static Interval<DateOnly> Add(this Interval<DateOnly> a, DateOnly b)
        {
            var (start, end) = a.ToNonNullableTuple();
            return a with
            {
                Start = start.AddDays(b.DayNumber - start.DayNumber),
                End = end.AddDays(b.DayNumber - end.DayNumber)
            };
        }

        public static Interval<DateOnly> AddDays(this Interval<DateOnly> value, int days)
        {
            var (start, end) = value.ToNonNullableTuple();
            return value with { Start = start.AddDays(days), End = end.AddDays(days) };
        }

        public static Interval<DateOnly> AddMonths(this Interval<DateOnly> value, int months)
        {
            var (start, end) = value.ToNonNullableTuple();
            return value with { Start = start.AddMonths(months), End = end.AddMonths(months) };
        }

        public static Interval<DateOnly> AddYears(this Interval<DateOnly> value, int years)
        {
            var (start, end) = value.ToNonNullableTuple();
            return value with { Start = start.AddYears(years), End = end.AddYears(years) };
        }

        private static (DateOnly, DateOnly) ToNonNullableTuple(this Interval<DateOnly> value)
        {
            

            var start = value.Start ?? DateOnly.MinValue;
            var end = value.End ?? DateOnly.MaxValue;
            return (start, end);
        }
    }
}
