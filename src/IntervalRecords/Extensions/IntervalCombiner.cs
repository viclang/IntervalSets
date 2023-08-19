namespace IntervalRecords.Extensions;
public static class IntervalCombiner
{
    /// <summary>
    /// Computes the union of two intervals if they overlap.
    /// </summary>
    /// <param name="other">The second interval to be unioned.</param>
    /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Union<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!first.IsConnected(second))
        {
            return null;
        }
        return first.Hull(second);
    }

    /// <summary>
    /// Computes the smallest interval that contains both input intervals.
    /// </summary>
    /// <param name="other">The other interval to compute the hull of.</param>
    /// <returns>The smallest interval that contains both input intervals.</returns>
    public static Interval<T> Hull<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        var minByStart = first.MinBy(second, i => i.Start);
        var maxByEnd = first.MaxBy(second, i => i.End);

        var startInclusive = first.Start == second.Start
            ? first.StartInclusive || second.StartInclusive
            : minByStart.StartInclusive;

        var endInclusive = first.End == second.End
            ? first.EndInclusive || second.EndInclusive
            : maxByEnd.EndInclusive;

        return IntervalFactory.Create(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
    }

    /// <summary>
    /// Computes the interval representing the portion of the interval that does not overlap with the other interval.
    /// </summary>
    /// <param name="other">The other interval</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public static Interval<T>? Except<T>(this Interval<T> first, Interval<T> second)
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!first.IsConnected(second))
        {
            return null;
        }

        var minByStart = first.MinBy(second, i => i.Start);
        var maxByStart = first.MaxBy(second, i => i.Start);

        var startInclusive = first.Start == second.Start
            ? first.StartInclusive || second.StartInclusive
            : minByStart.StartInclusive;

        var endInclusive = first.End == second.End
            ? first.EndInclusive || second.EndInclusive
            : maxByStart.EndInclusive;

        return IntervalFactory.Create(minByStart.Start, maxByStart.End, startInclusive, endInclusive);
    }


    /// <summary>
    /// Calculates the intersect of two intervals if they overlap.
    /// </summary>
    /// <param name="other">The other interval</param>
    /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Intersect<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!first.IsConnected(second))
        {
            return null;
        }
        var maxByStart = first.MaxBy(second, i => i.Start);
        var minByEnd = first.MinBy(second, i => i.End);

        var startInclusive = first.Start == second.Start
            ? first.StartInclusive && second.StartInclusive
            : maxByStart.StartInclusive;

        var endInclusive = first.End == second.End
            ? first.EndInclusive && second.EndInclusive
            : minByEnd.EndInclusive;

        return IntervalFactory.Create(maxByStart.Start, minByEnd.End, startInclusive, endInclusive);
    }

    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="second">The second interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T>? Gap<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (first.CompareStartToEnd(second) == 1)
        {
            var gap = IntervalFactory.Create(second.End, first.Start, !second.EndInclusive, !first.StartInclusive);
            return gap.IsEmpty ? null : gap;
        }
        if (first.CompareEndToStart(second) == -1)
        {
            var gap = IntervalFactory.Create(first.End, second.Start, !first.EndInclusive, !second.StartInclusive);
            return gap.IsEmpty ? null : gap;
        }
        return null;
    }

    /// <summary>
    /// Returns the minimum interval between two intervals, using a specific selector function to extract the value to compare.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
    /// <returns>The interval that is less than or equal to the other interval based on the comparison of the selected values.</returns>
    public static Interval<T> MinBy<T, TProperty>(this Interval<T> first, Interval<T> second, Func<Interval<T>, TProperty> selector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TProperty : IComparable<TProperty>
    {
        return selector(first).CompareTo(selector(second)) <= 0 ? first : second;
    }

    /// <summary>
    /// Returns the minimum interval between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    /// <returns>The interval that is less than or equal to the other interval.</returns>
    public static Interval<T> Min<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return first <= second ? first : second;
    }

    /// <summary>
    /// Returns the interval that is greater than or equal to the other interval, using a specific selector function to extract the value to compare.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
    /// <returns>The interval that is greater than or equal to the other interval based on the comparison of the selected values.</returns>
    public static Interval<T> MaxBy<T, TProperty>(this Interval<T> first, Interval<T> second, Func<Interval<T>, TProperty> selector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TProperty : IComparable<TProperty>
        => selector(first).CompareTo(selector(second)) >= 0 ? first : second;

    /// <summary>
    /// Returns the maximum interval between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    /// <returns>The interval that is greater than or equal to the other interval.</returns>
    public static Interval<T> Max<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => first >= second ? first : second;
}
