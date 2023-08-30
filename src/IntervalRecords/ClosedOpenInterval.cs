using Unbounded;

namespace IntervalRecords;
public sealed record ClosedOpenInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly ClosedOpenInterval<T> Empty = new(Unbounded<T>.NaN, Unbounded<T>.NaN);

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

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || (other.EndInclusive && other.End == Start);
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start <= other.End && other.Start < End
            || (other.StartInclusive && other.Start == End);
    }

    public override int CompareStart(Interval<T> other)
    {
        if (!other.StartInclusive && Start == other.Start)
        {
            return 1;
        }
        return Start.CompareTo(other.Start);
    }

    public override int CompareEnd(Interval<T> other)
    {
        if (other.EndInclusive && End == other.End)
        {
            return -1;
        }
        return End.CompareTo(other.End);
    }

    public override int CompareStartToEnd(Interval<T> other)
    {
        if (!other.EndInclusive && Start == other.End)
        {
            return 1;
        }
        return Start.CompareTo(other.End);
    }

    public override int CompareEndToStart(Interval<T> other)
    {
        return End > other.Start ? 1 : -1;
    }

    public override string ToString()
    {
        return $"[{Start}, {End})";
    }
}
