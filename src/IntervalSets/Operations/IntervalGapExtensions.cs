using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalGapExtensions
{
    /// <summary>
    /// Returns the gap between two intervals, or empty if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T> Gap<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return Interval<T>.Empty;
        }

        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start, Invert(right.EndBound), Invert(left.StartBound));
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start, Invert(left.EndBound), Invert(right.StartBound));
        }
        return Interval<T>.Empty;
    }

    /// <summary>
    /// Returns the gap between two intervals, or empty if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T, Closed, Closed> Gap<T>(this Interval<T, Open, Open> left, Interval<T, Open, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return Interval<T, Closed, Closed>.Empty;
        }

        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start);
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start);
        }
        return Interval<T, Closed, Closed>.Empty;
    }

    /// <summary>
    /// Returns the gap between two intervals, or empty if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T, Open, Open> Gap<T>(this Interval<T, Closed, Closed> left, Interval<T, Closed, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return Interval<T, Open, Open>.Empty;
        }

        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start);
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start);
        }
        return Interval<T, Open, Open>.Empty;
    }

    /// <summary>
    /// Returns the gap between two intervals, or empty if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T, Closed, Open> Gap<T>(this Interval<T, Closed, Open> left, Interval<T, Closed, Open> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return Interval<T, Closed, Open>.Empty;
        }

        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start);
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start);
        }
        return Interval<T, Closed, Open>.Empty;
    }

    /// <summary>
    /// Returns the gap between two intervals, or empty if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T, Open, Closed> Gap<T>(this Interval<T, Open, Closed> left, Interval<T, Open, Closed> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return Interval<T, Open, Closed>.Empty;
        }

        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start);
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start);
        }
        return Interval<T, Open, Closed>.Empty;
    }

    private static Bound Invert(this Bound bound)
    {
        return bound switch
        {
            Bound.Open => Bound.Closed,
            Bound.Closed => Bound.Open,
            _ => bound
        };
    }

    /// <summary>
    /// Returns the complement (or Gaps) of a collection of intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="value">The collection of intervals.</param>
    /// <returns>The complement of the collection of intervals, represented as a sequence of intervals.</returns>
    public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> value)
            where T : struct, IComparable<T>, ISpanParsable<T>
    {
        using var e = value.OrderBy(x => x.Start).GetEnumerator();

        if (!e.MoveNext())
            yield break;

        var previous = e.Current;
        while (e.MoveNext())
        {
            var result = previous.Gap(e.Current);
            if (!result.IsEmpty)
                yield return result;

            previous = e.Current;
        }
    }
}
