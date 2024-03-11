using IntervalRecords.Experiment.Helpers;

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

    /// <summary>
    /// Computes the smallest interval that contains all input intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The smallest interval that contains all input intervals, or null if the input is empty.</returns>
    public static Interval<T>? Hull<T>(
        this IEnumerable<Interval<T>> source)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (!source.Any())
        {
            return null;
        }
        var min = source.MinBy(i => i.Start)!;
        var max = source.MaxBy(i => i.End)!;

        return new Interval<T>(
            min.Start,
            max.End,
            min.StartInclusive,
            max.EndInclusive);
    }
}
