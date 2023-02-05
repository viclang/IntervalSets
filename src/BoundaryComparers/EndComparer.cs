using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.BoundaryComparers
{
    public class EndComparer<T> : IComparer<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>
    {
        public int Compare(Interval<T> x, Interval<T> y)
        {
            return x.CompareEnd(y);
        }
    }
}
