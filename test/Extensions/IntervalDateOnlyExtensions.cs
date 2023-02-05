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
        public static Interval<DateOnly> CreateBefore(this Interval<DateOnly> interval, int offset)
        {
            var end = interval.Start!.Value.AddDays(-offset);
            var start = end.AddDays(-interval.Length()!.Value);
            return interval with { Start = start, End = end };
        }

        public static Interval<DateOnly> CreateContains(this Interval<DateOnly> interval, int offset)
        {
            var start = interval.Start!.Value.AddDays(offset);
            var end = interval.End!.Value.AddDays(-offset);
            return interval with { Start = start, End = end };
        }

        public static Interval<DateOnly> CreateAfter(this Interval<DateOnly> interval, int offset)
        {
            var start = interval.End!.Value.AddDays(offset);
            var end = start.AddDays(interval.Length()!.Value);
            return interval with { Start = start, End = end };
        }
    }
}
