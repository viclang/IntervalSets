using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    internal static class IntervalComparison
    {
        internal static int CompareStart<T>(this Interval<T> x, Interval<T> y)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            if (x.Start is null && y.Start is null)
            {
                return 0;
            }
            if (x.Start is null && y.Start is not null)
            {
                return -1;
            }
            if (x.Start is not null && y.Start is null)
            {
                return 1;
            }
            return x.Start!.Value.CompareTo(y.Start!.Value);
        }

        internal static int CompareEnd<T>(this Interval<T> x, Interval<T> y)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            if (x.End is null && y.End is null)
            {
                return 0;
            }
            if (x.End is null && y.End is not null)
            {
                return 1;
            }
            if (x.End is not null && y.End is null)
            {
                return -1;
            }
            return x.End!.Value.CompareTo(y.End!.Value);
        }

        internal static int CompareEndToStart<T>(this Interval<T> x, Interval<T> y)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            if (x.End is null && y.Start is null)
            {
                return 0;
            }

            if (x.End is null && y.Start is not null)
            {
                return 1;
            }

            if (x.End is not null && y.Start is null)
            {
                return -1;
            }

            return x.End!.Value.CompareTo(y.Start!.Value);
        }

        internal static int CompareStartToEnd<T>(this Interval<T> x, Interval<T> y)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            if (x.Start is null && y.End is null)
            {
                return 0;
            }
            if (x.Start is null && y.End is not null)
            {
                return -1;
            }
            if (x.Start is not null && y.End is null)
            {
                return 1;
            }
            return x.Start!.Value.CompareTo(y.End!.Value);
        }
    }
}
