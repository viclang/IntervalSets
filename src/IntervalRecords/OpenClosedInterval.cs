using Unbounded;

namespace IntervalRecords;
public sealed record OpenClosedInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
{
    public static new readonly OpenClosedInterval<T> Empty = new(Unbounded<T>.None, Unbounded<T>.None);

    public static new readonly OpenClosedInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

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

    public bool Overlaps(OpenClosedInterval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public bool IsConnected(OpenClosedInterval<T> other)
    {
        return Start <= other.End && other.Start <= End;
    }

    public override string ToString()
    {
        return $"({Start}, {End}]";
    }
}
