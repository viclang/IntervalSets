using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.DataSets
{
    public interface IOffsetCreator<T, TOffset>
        where T : struct, IEquatable<T>, IComparable<T>
    {
        Interval<T> Before { get; init; }
        Interval<T> Contains { get; init; }
        Interval<T> After { get; init; }
    }
}
