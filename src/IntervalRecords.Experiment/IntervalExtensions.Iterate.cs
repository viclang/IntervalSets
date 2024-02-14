using IntervalRecords.Experiment;

namespace IntervalRecords.Experiment;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="source">The interval to generate values from.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public static IEnumerable<T> Iterate<T>(this Interval<T> source, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (source.Start is null)
        {
            return Enumerable.Empty<T>();
        }
        var start = source.StartInclusive ? source.Start : addStep(source.Start.Value);
        return source.Iterate(start.Value, addStep);
    }

    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="source">The interval to generate values from.</param>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public static IEnumerable<T> Iterate<T>(this Interval<T> source, T start, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (source.Contains(start) && !source.IsEmpty && source.End.HasValue)
        {
            for (var i = start; source.EndInclusive ? i.CompareTo(source.End.Value) <= 0 : i.CompareTo(source.End.Value) < 0; i = addStep(i))
            {
                yield return i;
            }
        }
    }
}