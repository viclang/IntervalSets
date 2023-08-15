namespace IntervalRecords.Extensions;
public static class IntervalOverlappingCalculator
{
    /// <summary>
    /// Determines interval overlapping relation between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    public static IntervalOverlapping GetOverlap<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => new ValueTuple<int, int>(first.CompareStart(second), first.CompareEnd(second)) switch
        {
            (0, 0) => IntervalOverlapping.Equal,
            (0, -1) => IntervalOverlapping.Starts,
            (1, -1) => IntervalOverlapping.ContainedBy,
            (1, 0) => IntervalOverlapping.Finishes,
            (-1, 0) => IntervalOverlapping.FinishedBy,
            (-1, 1) => IntervalOverlapping.Contains,
            (0, 1) => IntervalOverlapping.StartedBy,
            (-1, -1) => (IntervalOverlapping)first.CompareEndToStart(second) + (int)IntervalOverlapping.Meets,
            (1, 1) => (IntervalOverlapping)first.CompareStartToEnd(second) + (int)IntervalOverlapping.MetBy,
            (_, _) => throw new NotSupportedException()
        };
}
