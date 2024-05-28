using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalOverlapsExtensions
{
    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IInterval<T> left, IInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsEmpty || right.IsEmpty)
        {
            return false;
        }

        var startComparison = (left.StartBound.IsUnbounded() || right.EndBound.IsUnbounded())
            ? -1
            : left.Start.CompareTo(right.End);

        var endComparison = (right.StartBound.IsUnbounded() || left.EndBound.IsUnbounded())
            ? -1
            : right.Start.CompareTo(left.End);

        return startComparison < 0 && endComparison < 0
            || startComparison == 0 && left.StartBound.IsClosed() && right.EndBound.IsClosed()
            || endComparison == 0 && left.EndBound.IsClosed() && right.StartBound.IsClosed();
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Open, Open> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.EndBound.IsUnbounded() || left.Start.CompareTo(right.End) < 0
        && right.StartBound.IsUnbounded() || right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Closed> left, Interval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return left.Start.CompareTo(right.End) <= 0
            && right.Start.CompareTo(left.End) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Open> left, Interval<T, Closed, Open> right)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    => left.Start.CompareTo(right.End) < 0
    && right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Open, Closed> left, Interval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.Start.CompareTo(right.End) < 0
        && right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Open> left, Interval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.Start.CompareTo(right.End) <= 0
        && right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Closed> left, Interval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.Start.CompareTo(right.End) <= 0
        && right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Open> left, Interval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.Start.CompareTo(right.End) <= 0
        && right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Open, Unbounded> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.EndBound.IsUnbounded() || left.Start.CompareTo(right.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T, L>(this Interval<T, Closed, Unbounded> left, Interval<T, L, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        => left.Start.CompareTo(right.End) <= 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T, R>(this Interval<T, Unbounded, Open> left, IInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
        => left.EndBound.IsUnbounded() || right.Start.CompareTo(left.End) < 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T, R>(this Interval<T, Unbounded, Closed> left, Interval<T, Closed, R> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
        => right.Start.CompareTo(left.End) <= 0;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T, R>(this Interval<T, Unbounded, Unbounded> _, Interval<T> __)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
        => true;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T, R>(this Interval<T> _, Interval<T, Unbounded, Unbounded> __)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
        => true;

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T> left, Interval<T, Open, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Open, Closed> left, Interval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Open, Closed> left, Interval<T, Closed, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this Interval<T, Closed, Closed> left, Interval<T, Closed, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);
}
