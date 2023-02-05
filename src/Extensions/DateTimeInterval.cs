using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Extensions
{
    public static partial class IntervalExtensions
    {
        public static TimeSpan Length(this Interval<DateTime> interval)
        {
            var start = interval.Start ?? DateTime.MinValue;
            var end = interval.End ?? DateTime.MaxValue;
            return end.Subtract(start);
        }
    }
}
