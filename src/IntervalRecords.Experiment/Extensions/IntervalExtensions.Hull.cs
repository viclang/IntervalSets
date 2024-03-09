using IntervalRecords.Experiment.Extensions;

namespace IntervalRecords.Experiment;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Computes the smallest interval that contains both input intervals.
    /// </summary>
    /// <param name="other">The other interval to compute the hull of.</param>
    /// <returns>The smallest interval that contains both input intervals.</returns>
    public static Interval<T> Hull<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left == right)
        {
            return left;
        }
        var (start, end, startInclusive, endInclusive) = left;
        if (left.CompareStart(right) < 0)
        {
            start = right.Start;
            startInclusive = right.StartInclusive;
        }
        if (left.CompareEnd(right) > 0)
        {
            end = right.End;
            endInclusive = right.EndInclusive;
        }
        return new(start, end, startInclusive, endInclusive);
    }
}
