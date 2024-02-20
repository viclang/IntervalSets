namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Determines the bounded state of the interval.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to determine the bounded state of.</param>
    /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
    /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
    public static IntervalState GetState<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        => (value.Start.HasValue, value.End.HasValue) switch
        {
            (true, true) => IntervalState.Bounded,
            (false, false) => IntervalState.Unbounded,
            (false, true) => IntervalState.RightBounded,
            (true, false) => IntervalState.LeftBounded
        };

    /// <summary>
    /// Determines if the interval is half-bounded.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to check.</param>
    /// <returns>True if the interval is half-bounded.</returns>
    public static bool IsHalfBounded<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        => value.GetState() is IntervalState.LeftBounded or IntervalState.RightBounded;
}
