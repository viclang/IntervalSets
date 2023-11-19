using IntervalRecords.Extensions;
using System.Numerics;
using Unbounded;

namespace IntervalRecords.Extensions;
public static partial class IntervalCalculator
{
    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Unbounded<T> Length<T>(this Interval<T> source)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>, ISubtractionOperators<T, T, T>
        => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Unbounded<TimeSpan> Length(this Interval<DateTime> source) => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Unbounded<TimeSpan> Length(this Interval<DateTimeOffset> source) => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Unbounded<int> Length(this Interval<DateOnly> source) => Length(source, (left, right) => left.DayNumber - right.DayNumber);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Unbounded<TimeSpan> Length(this Interval<TimeOnly> source) => Length(source, (left, right) => left - right);

    private static Unbounded<TResult> Length<T, TResult>(Interval<T> source, Func<T, T, TResult> length)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (source.GetBoundaryState() is not BoundaryState.Bounded)
        {
            return Unbounded<TResult>.PositiveInfinity;
        }
        if (source.IsEmpty)
        {
            return default(TResult);
        }
        return length(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
    }
}
