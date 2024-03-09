namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Determines interval overlapping relation between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="first">The first interval to compare.</param>
    /// <param name="second">The second interval to compare.</param>
    public static IntervalRelation GetRelation<T>(this Interval<T> first, Interval<T> second)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => (first.CompareStart(second), first.CompareEnd(second)) switch
        {
            (0, 0) => IntervalRelation.Equal,
            (0, < 0) => IntervalRelation.Starts,
            (> 0, < 0) => IntervalRelation.ContainedBy,
            (> 0, 0) => IntervalRelation.Finishes,
            (< 0, 0) => IntervalRelation.FinishedBy,
            (< 0, > 0) => IntervalRelation.Contains,
            (0, > 0) => IntervalRelation.StartedBy,
            (< 0, < 0) => (IntervalRelation)first.CompareEndToStart(second) + (int)IntervalRelation.Meets,
            (> 0, > 0) => (IntervalRelation)first.CompareStartToEnd(second) + (int)IntervalRelation.MetBy
        };
}
