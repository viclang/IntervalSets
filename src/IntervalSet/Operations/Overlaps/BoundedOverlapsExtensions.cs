using IntervalSet.Bounds;
using IntervalSet.Types;

namespace IntervalSet.Core;
public static class BoundedOverlapsExtensions
{
    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = left.Start.CompareTo(right.End);
        var endComparison = right.Start.CompareTo(left.End);

        return startComparison < 0 && endComparison < 0
            || startComparison == 0 && left.StartBound.IsClosed() && right.EndBound.IsClosed()
            || endComparison == 0 && left.EndBound.IsClosed() && right.StartBound.IsClosed();
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Open, Open> left, IBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.ExclusiveOverlaps(right);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Closed> left, TypedBoundedInterval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return left.Start.CompareTo(right.End) <= 0
            && right.Start.CompareTo(left.End) <= 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Open> left, TypedBoundedInterval<T, Closed, Open> right)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    => left.ExclusiveOverlaps(right);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Open, Closed> left, TypedBoundedInterval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.ExclusiveOverlaps(right);

    private static bool ExclusiveOverlaps<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return left.Start.CompareTo(right.End) < 0
            && right.Start.CompareTo(left.End) < 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Open> left, TypedBoundedInterval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.OneSideInclusiveOverlaps(right);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Closed> left, TypedBoundedInterval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.OneSideInclusiveOverlaps(right);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Open> left, TypedBoundedInterval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => left.OneSideInclusiveOverlaps(right);

    private static bool OneSideInclusiveOverlaps<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return left.Start.CompareTo(right.End) <= 0
            && right.Start.CompareTo(left.End) < 0;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IBoundedInterval<T> left, TypedBoundedInterval<T, Open, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Open, Closed> left, TypedBoundedInterval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Open, Closed> left, TypedBoundedInterval<T, Closed, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this TypedBoundedInterval<T, Closed, Closed> left, TypedBoundedInterval<T, Closed, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => right.Overlaps(left);
}
