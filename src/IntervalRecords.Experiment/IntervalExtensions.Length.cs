using IntervalRecords.Experiment.Endpoints;
using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Endpoint<T> Length<T>(this Interval<T> source)
        where T : struct, INumber<T>
        => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Endpoint<TimeSpan> Length(this Interval<DateTime> source) => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Endpoint<TimeSpan> Length(this Interval<DateTimeOffset> source) => Length(source, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Endpoint<int> Length(this Interval<DateOnly> source) => Length(source, (left, right) => left.DayNumber - right.DayNumber);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static Endpoint<TimeSpan> Length(this Interval<TimeOnly> source) => Length(source, (left, right) => left - right);

    private static TResult? Length<T, TResult>(Interval<T> source, Func<T, T, TResult> length)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (source.GetIntervalState() is not IntervalState.Bounded)
        {
            return null;
        }
        if (source.IsEmpty)
        {
            return default(TResult);
        }
        return length(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
    }
}
