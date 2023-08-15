using Unbounded;

namespace IntervalRecords;
public sealed record OpenInterval<T> : Interval<T>
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

    public override bool Contains(T value)
    {
        return Start < value && value < End;
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End;
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || (other.EndInclusive && other.End == Start)
            || (other.StartInclusive && other.Start == End);
    }

    public override int CompareStart(Interval<T> other)
    {
        if (other.StartInclusive && Start == other.Start)
        {
            return -1;
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
        return Start < other.End ? -1 : 1;
    }

    public override int CompareEndToStart(Interval<T> other)
    {
        return other.Start < End ? 1 : -1;
    }

    public override string ToString()
    {
        return $"({Start}, {End})";
    }
}
