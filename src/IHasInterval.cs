using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public interface IHasInterval<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        Interval<T> Interval { get; }
    }
}
