using IntervalSet.Types;

namespace IntervalSet.Operations;
public static class BoundedIterateExtensions
{
    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="value">The interval to generate values from.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public static IEnumerable<T> Iterate<T>(this IInterval<T> value, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.StartBound.IsUnbounded() || value.EndBound.IsUnbounded())
        {
            return Enumerable.Empty<T>();
        }
        var start = value.StartBound.IsClosed() ? value.Start : addStep(value.Start);
        return value.Iterate(start, addStep);
    }

    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="value">The interval to generate values from.</param>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public static IEnumerable<T> Iterate<T>(this IInterval<T> value, T start, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (value.Contains(start) && !value.IsEmpty)
        {
            for (var i = start; value.EndBound.IsClosed() ? i.CompareTo(value.End) <= 0 : i.CompareTo(value.End) < 0; i = addStep(i))
            {
                yield return i;
            }
        }
    }
}