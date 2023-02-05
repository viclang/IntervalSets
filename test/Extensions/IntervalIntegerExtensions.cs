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
        public static Interval<int> CreateBefore(this Interval<int> interval, int offset)
        {
            var end = interval.Start!.Value - offset;
            var start = end - interval.Length();
            return interval with { Start = start, End = end };
        }

        private static Interval<int> CreateContains(this Interval<int> interval, int offset)
        {
            var end = interval.Start!.Value - offset;
            var start = end - interval.Length();
            return interval with { Start = start, End = end };
        }

        private static Interval<int> CreateAfter(this Interval<int> interval, int offset)
        {
            var start = interval.End + offset;
            var end = start + interval.Length();
            return interval with { Start = start, End = end };
        }
    }
}
