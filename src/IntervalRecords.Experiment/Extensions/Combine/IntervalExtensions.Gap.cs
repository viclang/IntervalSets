using IntervalRecords.Experiment.Extensions.Combine;
using IntervalRecords.Experiment.Helpers;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T>? Gap<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return null;
        }
        if (left.CompareStartToEnd(right) > 0)
        {
            return new(right.End, left.Start, !right.EndInclusive, !left.StartInclusive);
        }
        if (left.CompareEndToStart(right) < 0)
        {
            return new(left.End, right.Start, !left.EndInclusive, !right.StartInclusive);
        }
        return null;
    }

    /// <summary>
    /// Returns the complement (or Gaps) of a collection of intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The complement of the collection of intervals, represented as a sequence of intervals.</returns>
    public static IEnumerable<Interval<T>> Complement<T>(
        this IEnumerable<Interval<T>> source)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => source.Pairwise((a, b) => a.Gap(b));
}
