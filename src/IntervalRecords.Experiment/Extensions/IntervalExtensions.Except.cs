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
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
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
            yield return IntervalFactory.Create(left.Start, right.Start, left.StartInclusive, !right.StartInclusive);
        }
        else if (compareStart == 1)
        {
            yield return IntervalFactory.Create(right.Start, left.Start, right.StartInclusive, !left.StartInclusive);
        }
        var compareEnd = left.CompareEnd(right);
        if (compareEnd == -1)
        {
            yield return IntervalFactory.Create(left.End, right.End, !left.EndInclusive, right.EndInclusive);
        }
        else if (compareEnd == 1)
        {
            yield return IntervalFactory.Create(right.End, left.End, !right.EndInclusive, left.EndInclusive);
        }
    }
}
