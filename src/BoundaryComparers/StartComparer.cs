using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.BoundaryComparers
{
    internal class StartComparer<T> : IComparer<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public int Compare(Interval<T> x, Interval<T> y)
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
    }
}
