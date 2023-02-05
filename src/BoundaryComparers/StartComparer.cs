using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.BoundaryComparers
{
    public class StartComparer<T> : IComparer<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>
    {
        public int Compare(Interval<T> x, Interval<T> y)
        {
            return x.CompareStart(y);
        }
    }
}
