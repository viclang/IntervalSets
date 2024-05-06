using IntervalSet.Bounds;
using IntervalSet.Types;

namespace IntervalSet.Operations;
public static class BoundedIRelationExtensions
{
    /// <summary>
    /// Determines interval overlapping relation between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="left">The first interval to compare.</param>
    /// <param name="right">The second interval to compare.</param>
    public static IntervalRelation GetRelation<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => (left.CompareStart(right), left.CompareEnd(right)) switch
        {
            (0, 0) => IntervalRelation.Equal,
            (0, < 0) => IntervalRelation.Starts,
            ( > 0, < 0) => IntervalRelation.ContainedBy,
            ( > 0, 0) => IntervalRelation.Finishes,
            ( < 0, 0) => IntervalRelation.FinishedBy,
            ( < 0, > 0) => IntervalRelation.Contains,
            (0, > 0) => IntervalRelation.StartedBy,
            ( < 0, < 0) => (IntervalRelation)left.CompareEndToStart(right) + (int)IntervalRelation.Meets,
            ( > 0, > 0) => (IntervalRelation)left.CompareStartToEnd(right) + (int)IntervalRelation.MetBy
        };

    /// <summary>
    /// Compares the start of two intervals.
    /// </summary>
    /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
    private static int CompareStart<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => left.Start.CompareTo(right.Start) switch
        {
            0 => left.StartBound.IsClosed()
                .CompareTo(right.StartBound.IsClosed()),
            var comparison => comparison
        };

    /// <summary>
    /// Compares the end of two intervals.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    private static int CompareEnd<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => left.End.CompareTo(right.End) switch
        {
            0 => left.EndBound.IsClosed()
                .CompareTo(right.EndBound.IsClosed()),
            var comparison => comparison
        };

    /// <summary>
    /// Compares the start of the first interval with the end of the second interval.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    private static int CompareStartToEnd<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        int comparison = left.Start.CompareTo(right.End);
        if (comparison == 0 && (left.StartBound.IsOpen() || right.EndBound.IsOpen()))
        {
            return 1;
        }
        return comparison;
    }

    /// <summary>
    /// Compares the end of the first interval with the start of the second interval.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    private static int CompareEndToStart<T>(this IBoundedInterval<T> left, IBoundedInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        int comparison = left.End.CompareTo(right.Start);
        if (comparison == 0 && (left.EndBound.IsOpen() || right.StartBound.IsOpen()))
        {
            return -1;
        }
        return comparison;
    }
}
