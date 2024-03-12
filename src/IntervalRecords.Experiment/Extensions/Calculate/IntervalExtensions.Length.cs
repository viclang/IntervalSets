using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static T? Length<T>(this Interval<T> value)
        where T : struct, INumber<T>
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this Interval<DateTime> value)
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this Interval<DateTimeOffset> value)
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static int? Length(this Interval<DateOnly> value)
        => Length(value, (left, right) => left.DayNumber - right.DayNumber);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this Interval<TimeOnly> value) => Length(value, (left, right) => left - right);

    private static TResult? Length<T, TResult>(Interval<T> value, Func<T, T, TResult> length)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (value.Start is null || value.End is null)
        {
            return null;
        }
        if (value.IsEmpty)
        {
            return default(TResult);
        }
        return length(value.End.GetValueOrDefault(), value.Start.GetValueOrDefault());
    }
}
