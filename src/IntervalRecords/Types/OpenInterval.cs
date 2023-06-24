using IntervalRecords.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords;
public sealed record OpenInterval<T> : Interval<T>, IOverlaps<OpenInterval<T>>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly OpenInterval<T> Empty = new(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static new readonly OpenInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

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

    public bool Overlaps(OpenInterval<T> other)
    {
        return Overlaps((Interval<T>)other);
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || Start == other.End && other.EndInclusive
            || End == other.Start && other.StartInclusive;
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
        if (result == 0 && other.EndInclusive)
        {
            return -1;
        }
        return result;
    }

    protected override IntervalOverlapping CompareEndStart(Interval<T> other)
    {
        if (End.CompareTo(other.Start) <= 0)
        {
            return IntervalOverlapping.Before;
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
