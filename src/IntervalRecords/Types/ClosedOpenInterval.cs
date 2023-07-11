using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords.Types;
public sealed record ClosedOpenInterval<T> : Interval<T>, IOverlaps<ClosedOpenInterval<T>>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly ClosedOpenInterval<T> Empty = new(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static new readonly ClosedOpenInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override IntervalType IntervalType => IntervalType.ClosedOpen;

    public override bool StartInclusive => true;

    public override bool EndInclusive => false;

    public override bool IsSingleton => false;

    public ClosedOpenInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static ClosedOpenInterval<T> LeftBounded(T start) => new ClosedOpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public static ClosedOpenInterval<T> RightBounded(T end) => new ClosedOpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public override bool Contains(Unbounded<T> value)
    {
        return Start <= value && value < End;
    }

    public bool Overlaps(ClosedOpenInterval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || Start == other.End && other.EndInclusive;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start <= other.End && other.Start < End
            || End == other.Start && other.StartInclusive;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append('[')
            .Append(Start)
            .Append(", ")
            .Append(End)
            .Append(')')
            .ToString();
    }

    protected override int CompareStart(Interval<T> other)
    {
        var result = Start.CompareTo(other.Start);
        if (result == 0 && !other.StartInclusive)
        {
            return 1;
        }
        return result;
    }

    protected override int CompareEnd(Interval<T> other)
    {
        var result = End.CompareTo(other.End);
        if (result == 0 && other.EndInclusive)
        {
            return -1;
        }
        return result;
    }

    protected override IntervalOverlapping CompareStartEnd(Interval<T> other)
    {
        if (Start > other.End || Start == other.End && !other.EndInclusive)
        {
            return IntervalOverlapping.After;
        }
        if (Start == other.End)
        {
            return IntervalOverlapping.MetBy;
        }
        return IntervalOverlapping.OverlappedBy;
    }

    protected override IntervalOverlapping CompareEndStart(Interval<T> other)
    {
        if (End <= other.Start)
        {
            return IntervalOverlapping.Before;
        }
        return IntervalOverlapping.Overlaps;
    }
}
