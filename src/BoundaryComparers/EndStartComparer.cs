using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.BoundaryComparers
{
    internal class EndStartComparer<T> : IComparer<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public int Compare(Interval<T> x, Interval<T> y)
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
    }
}
