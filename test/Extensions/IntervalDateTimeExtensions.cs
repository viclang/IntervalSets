using IntervalRecord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.Extensions
{
    public static partial class IntervalExtensions
    {
        public static Interval<DateTime> CreateBefore(this Interval<DateTime> interval, TimeSpan offset)
        {
            var end = interval.Start - offset;
            var start = end - interval.Length();
            return interval with { Start = start, End = end };
        }

        public static Interval<DateTime> CreateContains(this Interval<DateTime> interval, TimeSpan offset)
        {
            var start = interval.Start + offset;
            var end = interval.End - offset;
            return interval with { Start = start, End = end };
        }

        public static Interval<DateTime> CreateAfter(this Interval<DateTime> interval, TimeSpan offset)
        {
            var start = interval.End + offset;
            var end = start + interval.Length();
            return interval with { Start = start, End = end };
        }
    }
}
