using Intervals.Bounds;

namespace Intervals.Types;
public sealed record OpenInterval<T>(T Start, T End) : TypedBoundedInterval<T, Open, Open>(Start, End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public static implicit operator OpenInterval<T>(Interval<T, Open, Open> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T, Open, Open>(OpenInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End);

    public static implicit operator Interval<T>(OpenInterval<T> closedInterval)
        => new(closedInterval.Start, closedInterval.End, BoundPair.Open);

    public override bool IsEmpty => End.CompareTo(Start) <= 0;

    public override string ToString() => $"({Start}, {End})";
}
