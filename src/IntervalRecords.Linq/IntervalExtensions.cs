namespace IntervalRecords.Linq;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Computes the union of a collection of intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The union of the collection of intervals.</returns>
    public static IEnumerable<Interval<T>> UnionAll<T>(this IEnumerable<Interval<T>> source)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return source.Reduce((a, b) => a.Union(b));
    }

    /// <summary>
    /// Computes the smallest interval that contains all input intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The smallest interval that contains all input intervals, or null if the input is empty.</returns>
    public static Interval<T>? Hull<T>(
        this IEnumerable<Interval<T>> source)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!source.Any())
        {
            return null;
        }
        var min = source.MinBy(i => i.Start)!;
        var max = source.MaxBy(i => i.End)!;

        return Interval.Create(
            min.Start,
            max.End,
            min.StartInclusive,
            max.EndInclusive);
    }

    /// <summary>
    /// Computes the collection of intervals representing the portions of the collection of intervals that do not overlap with each other.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The collection of intervals representing the portions of the collection of intervals that do not overlap with each other.</returns>
    public static IEnumerable<Interval<T>> ExcludeOverlap<T>(this IEnumerable<Interval<T>> source)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return source.Pairwise((a, b) => a.Except(b)).ToList().Where(i => !i.IsEmpty);
    }

    /// <summary>
    /// Returns an enumeration of intervals that represent the intersection of all overlapping intervals in the input sequence.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>An enumeration of intervals that represent the intersection of all overlapping intervals in the input sequence.</returns>
    public static IEnumerable<Interval<T>> IntersectAll<T>(
        this IEnumerable<Interval<T>> source)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return source.Pairwise((a, b) => a.Intersect(b)).Where(i => !i.IsEmpty);
    }

    /// <summary>
    /// Returns the complement (or Gaps) of a collection of intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <returns>The complement of the collection of intervals, represented as a sequence of intervals.</returns>
    public static IEnumerable<Interval<T>> Complement<T>(
        this IEnumerable<Interval<T>> source)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return source.Pairwise((a, b) => a.Gap(b));
    }
}
