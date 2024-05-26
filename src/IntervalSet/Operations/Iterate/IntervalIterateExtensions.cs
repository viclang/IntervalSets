using IntervalSet.Types;

namespace IntervalSet.Operations;
public static class IntervalIterateExtensions
{
    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="value">The interval to generate values from.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public static IEnumerable<T> Iterate<T>(this Interval<T> value, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (value.IsEmpty || value.StartBound.IsUnbounded() || value.EndBound.IsUnbounded())
        {
            return Enumerable.Empty<T>();
        }
        return value.InternalIterate(addStep);
    }

    private static IEnumerable<T> InternalIterate<T>(this Interval<T> value, Func<T, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        var isClosed = value.EndBound.IsClosed();
        var start = value.StartBound.IsClosed() ? value.Start : addStep(value.Start);
        for (var i = start; isClosed ? i.CompareTo(value.End) <= 0 : i.CompareTo(value.End) < 0; i = addStep(i))
        {
            yield return i;
        }
    }
}