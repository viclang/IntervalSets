using Unbounded;

namespace IntervalRecords;
public sealed record OpenInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
{
    public static new readonly OpenInterval<T> Empty = new(Unbounded<T>.None, Unbounded<T>.None);

    public static new readonly OpenInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override bool StartInclusive => false;

    public override bool EndInclusive => false;

    public OpenInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static OpenInterval<T> RightBounded(T end) => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

    public static OpenInterval<T> LeftBounded(T start) => new OpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public override bool Contains(T value)
    {
        return Start < value && value < End;
    }

    public bool Overlaps(OpenInterval<T> other)
    {
        return IsConnected(other);
    }

    public bool IsConnected(OpenInterval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override string ToString()
    {
        return $"({Start}, {End})";
    }
}
