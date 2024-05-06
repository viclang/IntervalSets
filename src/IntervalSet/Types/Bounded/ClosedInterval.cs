using Intervals.Bounds;

namespace Intervals.Types;
public sealed record ClosedInterval<T>(T Start, T End) : TypedBoundedInterval<T, Closed, Closed>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public override bool IsEmpty => End.CompareTo(Start) < 0;

    public static implicit operator ClosedInterval<T>(Interval<T, Closed, Closed> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T, Closed, Closed>(ClosedInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T>(ClosedInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End, BoundPair.Closed);

    public override string ToString() => $"[{Start}, {End}]";
}
