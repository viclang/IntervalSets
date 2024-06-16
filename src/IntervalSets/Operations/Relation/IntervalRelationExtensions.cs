using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalRelationExtensions
{
    /// <summary>
    /// Determines interval overlapping relation between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="left">The first interval to compare.</param>
    /// <param name="right">The second interval to compare.</param>
    public static IntervalRelation GetRelation<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsEmpty && right.IsEmpty) return IntervalRelation.BothEmpty;
        if (left.IsEmpty) return IntervalRelation.FirstEmpty;
        if (right.IsEmpty) return IntervalRelation.SecondEmpty;

        return (left.CompareStart(right), left.CompareEnd(right)) switch
        {
            (0, 0) => IntervalRelation.Equals,
            (0, < 0) => IntervalRelation.Starts,
            ( > 0, < 0) => IntervalRelation.ContainedBy,
            ( > 0, 0) => IntervalRelation.Finishes,
            ( < 0, 0) => IntervalRelation.FinishedBy,
            ( < 0, > 0) => IntervalRelation.Contains,
            (0, > 0) => IntervalRelation.StartedBy,
            ( < 0, < 0) => (IntervalRelation)left.CompareEndToStart(right) + (int)IntervalRelation.Meets,
            ( > 0, > 0) => (IntervalRelation)left.CompareStartToEnd(right) + (int)IntervalRelation.MetBy
        };
    }
}
