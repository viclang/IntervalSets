using IntervalSet.Types;

namespace IntervalSet.Operations;
public static class BoundedRelationExtensions
{
    /// <summary>
    /// Determines interval overlapping relation between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="left">The first interval to compare.</param>
    /// <param name="right">The second interval to compare.</param>
    public static IntervalRelation GetRelation<T>(this IInterval<T> left, IInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsEmpty && right.IsEmpty) return IntervalRelation.BothEmpty;
        if (left.IsEmpty) return IntervalRelation.FirstEmpty;
        if (right.IsEmpty) return IntervalRelation.SecondEmpty;

        return (left.CompareStart(right), left.CompareEnd(right)) switch
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
    }

    /// <summary>
    /// Compares the start of two intervals.
    /// </summary>
    /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
    private static int CompareStart<T>(this IInterval<T> left, IInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        return (left.StartBound.IsUnbounded(), right.StartBound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => -1,
            (false, true) => 1,
            (false, false) =>
                left.Start.CompareTo(right.Start) switch
                {
                    0 => left.StartBound.CompareTo(right.StartBound),
                    var comparison => comparison
                }
        };
    }

    /// <summary>
    /// Compares the end of two intervals.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    private static int CompareEnd<T>(this IInterval<T> left, IInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        return (left.EndBound.IsUnbounded(), right.EndBound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => 1,
            (false, true) => -1,
            (false, false) =>
                left.End.CompareTo(right.End) switch
                {
                    0 => left.EndBound.CompareTo(right.EndBound),
                    var comparison => comparison
                }
        };
    }

    /// <summary>
    /// Compares the start of the first interval with the end of the second interval.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    private static int CompareStartToEnd<T>(this IInterval<T> left, IInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left.StartBound.IsUnbounded() || right.EndBound.IsUnbounded())
        {
            return -1;
        }

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
    private static int CompareEndToStart<T>(this IInterval<T> left, IInterval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left.EndBound.IsUnbounded() || right.StartBound.IsUnbounded())
        {
            return 1;
        }

        int comparison = left.End.CompareTo(right.Start);
        if (comparison == 0 && (left.EndBound.IsOpen() || right.StartBound.IsOpen()))
        {
            return -1;
        }
        return comparison;
    }
}
