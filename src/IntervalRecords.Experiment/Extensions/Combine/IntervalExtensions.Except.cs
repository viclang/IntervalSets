using IntervalRecords.Experiment.Extensions.Combine;
using IntervalRecords.Experiment.Helpers;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Produces the difference between two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public static IEnumerable<Interval<T>> Except<T>(this Interval<T> left, Interval<T> right)
    where T : struct, IComparable<T>, ISpanParsable<T>
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
            yield return new(left.Start, right.Start, left.StartInclusive, !right.StartInclusive);
        }
        else if (compareStart == 1)
        {
            yield return new(right.Start, left.Start, right.StartInclusive, !left.StartInclusive);
        }
        var compareEnd = left.CompareEnd(right);
        if (compareEnd == -1)
        {
            yield return new(left.End, right.End, !left.EndInclusive, right.EndInclusive);
        }
        else if (compareEnd == 1)
        {
            yield return new(right.End, left.End, !right.EndInclusive, left.EndInclusive);
        }
    }

    /// <summary>
    /// Computes the collection of intervals representing the portions of the collection of intervals that do not overlap with each other.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The collection of intervals representing the portions of the collection of intervals that do not overlap with each other.</returns>
    public static IEnumerable<Interval<T>> ExceptOverlap<T>(this IEnumerable<Interval<T>> source)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => source.Pairwise((a, b) => a.Except(b)).SelectMany(x => x).ToList().Where(i => !i.IsEmpty);
}
