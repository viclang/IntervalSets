namespace IntervalRecords;
public abstract partial record Interval<T>
{
    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public IEnumerable<T> Iterate(Func<T, T> addStep)
    {
        if (Start.IsNegativeInfinity)
        {
            return Enumerable.Empty<T>();
        }
        var start = StartInclusive ? Start.GetFiniteOrDefault() : addStep(Start.GetFiniteOrDefault());
        return Iterate(start, addStep);
    }

    /// <summary>
    /// Generates a sequence of values within the interval by incrementing the starting value using the given step function.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    /// <param name="source">The interval to generate values from.</param>
    /// <param name="start">The starting value of the sequence.</param>
    /// <param name="addStep">The step function to increment the starting value with.</param>
    /// <returns>The sequence of values generated within the interval.</returns>
    public IEnumerable<T> Iterate(T start, Func<T, T> addStep)
    {
        if (Contains(start) && !IsEmpty && !End.IsPositiveInfinity)
        {
            for (var i = start; EndInclusive ? i <= End : i < End; i = addStep(i))
            {
                yield return i;
            }
        }
    }
}
