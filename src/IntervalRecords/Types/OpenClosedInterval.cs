using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords.Types;
public sealed record OpenClosedInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public override bool StartInclusive => false;

    public override bool EndInclusive => true;

    public OpenClosedInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public override bool Contains(Unbounded<T> value)
    {
        throw new NotImplementedException();
    }

    public override bool IsEmpty() => !IsValid || Start == End;

    public override bool IsSingleton() => false;

    public override IntervalType GetIntervalType() => IntervalType.OpenClosed;
}
