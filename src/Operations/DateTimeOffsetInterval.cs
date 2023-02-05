using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Extensions
{
    public static partial class DateInterval
    {
        public static TimeSpan Length(this Interval<DateTimeOffset> interval)
        {
            var start = interval.Start ?? DateTimeOffset.MinValue;
            var end = interval.End ?? DateTimeOffset.MaxValue;
            return end.Subtract(start);
        }

        public static TimeSpan Radius(this Interval<DateTimeOffset> interval)
        {
            return interval.Length() / 2;
        }

        //public static DateTimeOffset Centre(this Interval<DateTimeOffset> interval)
        //{
        //    var start = interval.Start ?? DateTimeOffset.MinValue;
        //    var end = interval.End ?? DateTimeOffset.MaxValue;
        //    return (start.Add() / 2;
        //}

        //public static Interval<int> Add(Interval<DateTimeOffset> a, Interval<DateTimeOffset> b)
        //{
        //    return a with { Start = a.Start + b.Start, End = a.End + b.End };
        //}

        //public static Interval<int> Add(Interval<DateTimeOffset> a, TimeSpan b)
        //{
        //    return a with { Start = a.Start + b, End = a.End + b };
        //}

        //public static Interval<int> Substract(Interval<DateTimeOffset> a, TimeSpan b)
        //{
        //    return a with { Start = a.Start - b.Start, End = a.End - b.End };
        //}

        //public static Interval<int> Substract(Interval<DateTimeOffset> a, TimeSpan b)
        //{
        //    return a with { Start = a.Start - b, End = a.End - b };
        //}
    }
}
