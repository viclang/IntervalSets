using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public static class IntIntervalOverlappingExtensions
    {
        public static Interval<int> GetBefore(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start - offset - value.Length(), End = value.Start - offset };
        }

        public static Interval<int> GetMeets(this Interval<int> value)
        {
            return value with { Start = value.Start - value.Length(), End = value.Start };
        }

        public static Interval<int> GetOverlaps(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start - offset, End = value.Start + offset };
        }

        public static Interval<int> GetStarts(this Interval<int> value, int offset)
        {
            return value with { End = value.End - offset };
        }

        public static Interval<int> GetContainedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start + offset, End = value.End - offset };
        }

        public static Interval<int> GetFinishes(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start + offset };
        }

        public static Interval<int> GetFinishedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start - offset };
        }

        public static Interval<int> GetContains(this Interval<int> value, int offset)
        {
            return value with { Start = value.Start - offset, End = value.End + offset };
        }

        public static Interval<int> GetStartedBy(this Interval<int> value, int offset)
        {
            return value with { End = value.End + offset };
        }

        public static Interval<int> GetOverlappedBy(this Interval<int> value, int offset)
        {
            return value with { Start = value.End - offset, End = value.End + offset };
        }

        public static Interval<int> GetMetBy(this Interval<int> value)
        {
            return value with { Start = value.End, End = value.End + value.Length() };
        }

        public static Interval<int> GetAfter(this Interval<int> value, int offset)
        {
            return value with { Start = value.End + offset, End = value.End + offset + value.Length() };
        }
    }
}
