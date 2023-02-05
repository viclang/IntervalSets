using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.DataSets
{
    public interface IOffsetCreator<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        Interval<T> Before { get; init; }
        Interval<T> ContainedBy { get; init; }
        Interval<T> After { get; init; }
    }
}
