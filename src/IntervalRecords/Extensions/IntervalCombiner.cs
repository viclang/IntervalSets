namespace IntervalRecords;
public abstract partial record Interval<T>
{
    /// <summary>
    /// Computes the union of two intervals if they overlap.
    /// </summary>
    /// <param name="other">The second interval to be unioned.</param>
    /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
    public Interval<T>? Union(Interval<T> other)
    {
        if (!IsConnected(other))
        {
            return null;
        }
        return Hull(other);
    }

    /// <summary>
    /// Computes the smallest interval that contains both input intervals.
    /// </summary>
    /// <param name="other">The other interval to compute the hull of.</param>
    /// <returns>The smallest interval that contains both input intervals.</returns>
    public Interval<T> Hull(Interval<T> other)
    {
        var minByStart = MinBy(other, i => i.Start);
        var maxByEnd = MaxBy(other, i => i.End);

        var startInclusive = Start == other.Start
            ? StartInclusive || other.StartInclusive
            : minByStart.StartInclusive;

        var endInclusive = End == other.End
            ? EndInclusive || other.EndInclusive
            : maxByEnd.EndInclusive;

        return Interval.Create(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
    }

    /// <summary>
    /// Computes the interval representing the portion of the interval that does not overlap with the other interval.
    /// </summary>
    /// <param name="other">The other interval</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public Interval<T>? Except(Interval<T> other)
    {
        if (!IsConnected(other))
        {
            return null;
        }
        var minByStart = MinBy(other, i => i.Start);
        var maxByStart = MaxBy(other, i => i.Start);

        var startInclusive = Start == other.Start
            ? StartInclusive || other.StartInclusive
            : minByStart.StartInclusive;

        var endInclusive = End == other.End
            ? EndInclusive || other.EndInclusive
            : maxByStart.EndInclusive;

        return Interval.Create(minByStart.Start, maxByStart.Start, startInclusive, endInclusive);
    }

    /// <summary>
    /// Calculates the intersect of two intervals if they overlap.
    /// </summary>
    /// <param name="other">The other interval</param>
    /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
    public Interval<T>? Intersect(Interval<T> other)
    {
        if (!IsConnected(other))
        {
            return null;
        }
        var maxByStart = MaxBy(other, i => i.Start);
        var minByEnd = MinBy(other, i => i.End);

        var startInclusive = Start == other.Start
            ? StartInclusive && other.StartInclusive
            : maxByStart.StartInclusive;

        var endInclusive = End == other.End
            ? EndInclusive && other.EndInclusive
            : minByEnd.EndInclusive;

        return Interval.Create(maxByStart.Start, minByEnd.End, startInclusive, endInclusive);
    }

    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="other">The second interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public Interval<T>? Gap(Interval<T> other)
    {
        if (Start > other.End || (Start == other.End && !StartInclusive && !other.EndInclusive))
        {
            return Interval.Create(other.End, Start, !other.EndInclusive, !StartInclusive);
        }
        if (End < other.Start || (End == other.Start && !EndInclusive && !other.StartInclusive))
        {
            return Interval.Create(End, other.Start, !EndInclusive, !other.StartInclusive);
        }
        return null;
    }
}
