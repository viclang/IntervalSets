using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords.Types;
public sealed record ClosedOpenInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly ClosedInterval<T> Empty = new ClosedInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static readonly ClosedInterval<T> Unbounded = new ClosedInterval<T>(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override IntervalType IntervalType => IntervalType.ClosedOpen;

    public override bool StartInclusive => true;

    public override bool EndInclusive => false;

    public override bool IsEmpty => !IsValid;

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

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || Start == other.End && other.EndInclusive;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return other.StartInclusive && Start <= other.End && other.Start <= End;
    }
}
