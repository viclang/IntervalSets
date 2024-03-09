using IntervalRecords.Experiment.Helpers;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the intersect of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Intersect<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (!left.Overlaps(right))
        {
            return null;
        }
        if (left == right)
        {
            return left;
        }
        var (start, end, startInclusive, endInclusive) = left;
        if (left.CompareStart(right) == -1)
        {
            start = right.Start;
            startInclusive = right.StartInclusive;
        }
        if (left.CompareEnd(right) == 1)
        {
            end = right.End;
            endInclusive = right.EndInclusive;
        }
        return new(start, end, startInclusive, endInclusive);
    }
}
