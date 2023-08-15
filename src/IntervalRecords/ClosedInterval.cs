using Unbounded;

namespace IntervalRecords;
public sealed record ClosedInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public static new readonly ClosedInterval<T> Empty = new(Unbounded<T>.NaN, Unbounded<T>.NaN);

    public static new readonly ClosedInterval<T> Unbounded = new(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

    public override IntervalType IntervalType => IntervalType.Closed;

    /// <summary>
    /// Gets a value indicating whether the start of the interval is inclusive.
    /// The Start of a ClosedInterval is inclusive, except when it is Unbounded.
    /// </summary>
    public override bool StartInclusive => true;

    /// <summary>
    /// Gets a value indicating whether the end of the interval is inclusive.
    /// The End of a ClosedInterval is inclusive, except when it is Unbounded.
    /// </summary>
    public override bool EndInclusive => true;

    public override bool IsValid => Start < End && !Start.IsNaN || Start == End;

    public override bool IsEmpty => !IsValid;

    public override bool IsSingleton => Start == End;

    /// <summary>
    /// Creates a Closed interval.
    /// </summary>
    /// <param name="start">Represents the start value of the interval.</param>
    /// <param name="end">Represents the end value of the interval.</param>
    public ClosedInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public static ClosedInterval<T> Singleton(T value) => new ClosedInterval<T>(value, value);

    public static ClosedInterval<T> LeftBounded(T start) => new ClosedInterval<T>(start, Unbounded<T>.PositiveInfinity);

    public static ClosedInterval<T> RightBounded(T end) => new ClosedInterval<T>(Unbounded<T>.NegativeInfinity, end);

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public override bool Contains(T value)
    {
        return Start <= value && value <= End;
    }

    public override bool Overlaps(Interval<T> other)
    {
        return Start < other.End && other.Start < End
            || (other.EndInclusive && Start == other.End)
            || (other.StartInclusive && End == other.Start);
    }

    public override bool IsConnected(Interval<T> other)
    {
        return Start <= other.End && other.Start <= End;
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
        if (!other.EndInclusive && End == other.End)
        {
            return 1;
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
        if (!other.StartInclusive && End == other.Start)
        {
            return -1;
        }
        return End.CompareTo(other.Start);
    }

    public static bool operator >(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left.End >= right.End && right.Start > left.Start;
    }

    public static bool operator <(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left.End <= right.End && right.Start < left.Start;
    }

    public static bool operator >=(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left == right || left > right;
    }

    public static bool operator <=(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left == right || left < right;
    }

    public override string ToString()
    {
        return $"[{Start}, {End}]";
    }
}
