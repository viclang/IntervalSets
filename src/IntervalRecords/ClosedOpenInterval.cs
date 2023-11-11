using Unbounded;

namespace IntervalRecords;
public sealed record ClosedOpenInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
{
    public static new readonly ClosedOpenInterval<T> Empty = new(Unbounded<T>.None, Unbounded<T>.None);

    public static new readonly ClosedOpenInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override bool StartInclusive => true;

    public override bool EndInclusive => false;

    public override bool IsSingleton => false;

    public ClosedOpenInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static ClosedOpenInterval<T> LeftBounded(T start) => new ClosedOpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public static ClosedOpenInterval<T> RightBounded(T end) => new ClosedOpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public override bool Contains(T value)
    {
        return Start <= value && value < End;
    }

    public bool Overlaps(ClosedOpenInterval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public bool IsConnected(ClosedOpenInterval<T> other)
    {
        return Start <= other.End && other.Start <= End;
    }

    public override string ToString()
    {
        return $"[{Start}, {End})";
    }
}
