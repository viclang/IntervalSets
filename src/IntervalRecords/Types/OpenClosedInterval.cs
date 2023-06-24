using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords.Types;
public sealed record OpenClosedInterval<T> : Interval<T>, IOverlaps<OpenClosedInterval<T>>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly OpenClosedInterval<T> Empty = new(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static new readonly OpenClosedInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override IntervalType IntervalType => IntervalType.OpenClosed;

    public override bool StartInclusive => false;

    public override bool EndInclusive => true;

    public OpenClosedInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static OpenClosedInterval<T> RightBounded(T end) => new OpenClosedInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public static OpenClosedInterval<T> LeftBounded(T start) => new OpenClosedInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public override bool Contains(Unbounded<T> value)
    {
        return Start < value && value <= End;
    }

    public bool Overlaps(OpenClosedInterval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || End == other.Start && other.StartInclusive;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start < other.End && other.Start <= End
            || Start == other.End && other.EndInclusive;
    }

    protected override int CompareStart(Interval<T> other)
    {
        var result = Start.CompareTo(other.Start);
        if (result == 0 && other.StartInclusive)
        {
            return -1;
        }
        return result;
    }

    protected override int CompareEnd(Interval<T> other)
    {
        var result = End.CompareTo(other.End);
        if (result == 0 && !other.EndInclusive)
        {
            return 1;
        }
        return result;
    }

    protected override IntervalOverlapping CompareEndStart(Interval<T> other)
    {
        if (End < other.Start || (End == other.Start && !other.StartInclusive))
        {
            return IntervalOverlapping.Before;
        }
        if (End == other.Start)
        {
            return IntervalOverlapping.Meets;
        }
        return IntervalOverlapping.Overlaps;
    }

    protected override IntervalOverlapping CompareStartEnd(Interval<T> other)
    {
        if (Start.CompareTo(other.End) >= 0)
        {
            return IntervalOverlapping.After;
        }
        return IntervalOverlapping.OverlappedBy;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
