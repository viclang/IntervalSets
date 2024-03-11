namespace IntervalRecords.Experiment.Extensions.Combine;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns the sequence of intervals resulting from combining adjacent intervals in the collection of intervals
    /// using the specified result selector function.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="source">The collection of intervals.</param>
    /// <param name="resultSelector">The function used to combine adjacent intervals.</param>
    /// <returns>The reduced sequence of intervals.</returns>
    public static IEnumerable<Interval<T>> Reduce<T>(
        this IEnumerable<Interval<T>> source,
        Func<Interval<T>, Interval<T>, Interval<T>?> resultSelector)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        using var e = source.OrderBy(x => x.Start).GetEnumerator();

        if (!e.MoveNext())
            yield break;

        var previous = e.Current;
        while (e.MoveNext())
        {
            var result = resultSelector(previous, e.Current);
            if (result == null)
            {
                yield return previous;
                previous = e.Current;
            }
            else
            {
                previous = result;
            }
        }
        yield return previous;
    }
}
