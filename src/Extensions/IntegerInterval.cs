using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Extensions
{
    public static partial class IntervalExtensions
    {
        public static int? Length(this Interval<int> value)
        {
            if (!value.IsBounded())
            {
                return null;
            }

            if (value.IsEmpty())
            {
                return 0;
            }

            var start = value.Start ?? int.MinValue;
            var end = value.End ?? int.MaxValue;
            return end - start;
        }

        public static double? Radius(this Interval<int> value)
        {
            if (!value.IsBounded() || value.IsEmpty())
            {
                return null;
            }

            return value.Length() / 2;
        }

        public static double? Centre(this Interval<int> value)
        {
            if (!value.IsBounded() || value.IsEmpty())
            {
                return null;
            }

            var start = value.Start ?? int.MinValue;
            var end = value.End ?? int.MaxValue;
            return (start + end) / 2;
        }

        public static Interval<int> Add(Interval<int> a, Interval<int> b)
        {
            if (b.IsEmpty())
            {
                return a;
            }

            return a with { Start = a.Start + b.Start, End = a.End + b.End };
        }

        public static Interval<int> Add(Interval<int> a, int b)
        {
            return a with { Start = a.Start + b, End = a.End + b };
        }

        public static Interval<int> Substract(Interval<int> a, Interval<int> b)
        {
            if (b.IsEmpty())
            {
                return a;
            }

            return a with { Start = a.Start - b.Start, End = a.End - b.End };
        }

        public static Interval<int> Substract(Interval<int> a, int b)
        {
            return a with { Start = a.Start - b, End = a.End - b };
        }
    }
}
