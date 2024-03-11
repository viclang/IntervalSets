using IntervalRecords.Experiment.Extensions.Combine;

namespace IntervalRecords.Experiment;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Computes the union of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Union<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsDisjoint(right))
        {
            return null;
        }
        return left.Hull(right);
    }

    /// <summary>
    /// Computes the union of a collection of intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The union of the collection of intervals.</returns>
    public static IEnumerable<Interval<T>> UnionAll<T>(this IEnumerable<Interval<T>> source)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => source.Reduce((a, b) => a.Union(b));
}
