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

    public override bool Contains(T value)
    {
        return Start < value && value <= End;
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || (other.StartInclusive && other.Start == End);
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start <= other.End && other.Start < End
            || (other.EndInclusive && other.End == Start);
    }

    protected override int CompareStart(Interval<T> other)
    {
        if (other.StartInclusive && Start == other.Start)
        {
            return -1;
        }
        return Start.CompareTo(other.Start);
    }

    protected override int CompareEnd(Interval<T> other)
    {
        if (!other.EndInclusive && End == other.End)
        {
            return 1;
        }
        return End.CompareTo(other.End);
    }

    public override IntervalOverlapping CompareStartToEnd(Interval<T> other)
    {
        return Start >= other.End ? IntervalOverlapping.After : IntervalOverlapping.OverlappedBy;
    }

    public override IntervalOverlapping CompareEndToStart(Interval<T> other)
    {
        if (!other.StartInclusive && End == other.Start)
        {
            return IntervalOverlapping.Before;
        }
        return (IntervalOverlapping)End.CompareTo(other.Start) + (int)IntervalOverlapping.Meets;
    }

    public override string ToString()
    {
        return $"({Start}, {End}]";
    }
}
