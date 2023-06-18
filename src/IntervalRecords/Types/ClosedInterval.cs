using System.Text;
using Unbounded;

namespace IntervalRecords;
public sealed record ClosedInterval<T> : Interval<T>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public override bool StartInclusive => true;

    public override bool EndInclusive => true;

    /// <summary>
    /// Creates a Closed interval.
    /// </summary>
    /// <param name="start">Represents the start value of the interval.</param>
    /// <param name="end">Represents the end value of the interval.</param>
    public ClosedInterval(Unbounded<T> start, Unbounded<T> end) : base(start, end)
    {
    }

    public override IntervalType GetIntervalType() => IntervalType.Closed;

    /// <summary>
    /// Indicates whether an interval is empty.
    /// </summary>
    /// <returns>True if the interval is Invalid or the interval is not <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal</returns>
    public override bool IsEmpty() => !IsValid;

    /// <summary>
    /// Indicates whether an interval is Singleton.
    /// </summary>
    /// <returns>True if the interval is <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal.</returns>
    public override bool IsSingleton() => Start == End;

    public bool Overlaps(ClosedInterval<T> other)
    {
        return Start <= other.End && other.Start <= End;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval contains the specified value.
    /// </summary>
    /// <param name="value">The value to check if it is contained by the current interval</param>
    /// <returns></returns>
    public override bool Contains(Unbounded<T> value)
    {
        return Start <= value && value <= End;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append('[')
            .Append(Start)
            .Append(", ")
            .Append(End)
            .Append(']')
            .ToString();
    }

    public static bool operator >(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left.End >= right.End && left.Start < right.Start;
    }

    public static bool operator <(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left.End <= right.End && left.Start > right.Start;
    }

    public static bool operator >=(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left == right || left > right;
    }

    public static bool operator <=(ClosedInterval<T> left, ClosedInterval<T> right)
    {
        return left == right || left < right;
    }
}
