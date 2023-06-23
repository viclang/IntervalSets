using IntervalRecords.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords;
public sealed record OpenInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly ClosedInterval<T> Empty = new ClosedInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static readonly ClosedInterval<T> Unbounded = new ClosedInterval<T>(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override IntervalType IntervalType => IntervalType.Open;

    public override bool StartInclusive => false;

    public override bool EndInclusive => false;

    public OpenInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static OpenInterval<T> RightBounded(T end) => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public static OpenInterval<T> LeftBounded(T start) => new OpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public override bool Contains(Unbounded<T> value)
    {
        return Start < value && value < End;
    }


    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return other.GetIntervalType() == IntervalType.Closed && Start <= other.End && other.Start <= End;
    }
}
