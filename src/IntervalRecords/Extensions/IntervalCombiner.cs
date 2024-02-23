using System.Collections;
using System.Collections.Generic;
using Unbounded;

namespace IntervalRecords.Extensions;
public static class IntervalCombiner
{

    /// <summary>
    /// Produces the difference between two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public static IEnumerable<Interval<T>> Except<T>(this Interval<T> left, Interval<T> right)
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (left == right)
        {
            yield break;
        }
        if (!left.Overlaps(right))
        {
            yield return left;
            yield return right;
            yield break;
        }
        var compareStart = left.CompareStart(right);
        if (compareStart == -1)
        {
            yield return IntervalFactory.Create(left.Start, right.Start, left.StartInclusive, !right.StartInclusive);
        }
        else if (compareStart == 1)
        {
            yield return IntervalFactory.Create(right.Start, left.Start, right.StartInclusive, !left.StartInclusive);
        }
        var compareEnd = left.CompareEnd(right);        
        if (compareEnd == -1)
        {
            yield return IntervalFactory.Create(left.End, right.End, !left.EndInclusive, right.EndInclusive);
        }
        else if (compareEnd == 1)
        {
            yield return IntervalFactory.Create(right.End, left.End, !right.EndInclusive, left.EndInclusive);
        }
    }

    /// <summary>
    /// Calculates the intersect of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Intersect<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (!left.Overlaps(right))
        {
            return null;
        }
        if (left == right)
        {
            return left;
        }
        var (start, end, startInclusive, endInclusive) = left;
        if (left.CompareStart(right) == -1)
        {
            start = right.Start;
            startInclusive = right.StartInclusive;
        }
        if (left.CompareEnd(right) == 1)
        {
            end = right.End;
            endInclusive = right.EndInclusive;
        }
        return IntervalFactory.Create(start, end, startInclusive, endInclusive);
    }

    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T>? Gap<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsConnected(right))
        {
            return null;
        }
        if (left.CompareStartToEnd(right) == 1)
        {
            return IntervalFactory.Create(right.End, left.Start, !right.EndInclusive, !left.StartInclusive);
        }
        if (left.CompareEndToStart(right) == -1)
        {
            return IntervalFactory.Create(left.End, right.Start, !left.EndInclusive, !right.StartInclusive);
        }
        return null;
    }

    public static Unbounded<T> MinStart<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return left.CompareStart(right) == -1 ? left.Start : right.Start;
    }

    public static Unbounded<T> MaxStart<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return left.CompareStart(right) == 1 ? left.Start : right.Start;
    }

    public static Unbounded<T> MinEnd<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return left.CompareEnd(right) == -1 ? left.Start : right.Start;
    }

    public static Unbounded<T> MaxEnd<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        return left.CompareEnd(right) == 1 ? left.Start : right.Start;
    }
}
