using IntervalSets.Types;
using System.Numerics;

namespace IntervalSets.Operations;
public static partial class IntervalLengthExtensions
{

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The length of the interval</returns>
    public static T Length<T>(this IInterval<T> value, T? step = null)
        where T : struct, INumber<T>
    {
        step ??= T.One;
        return value.Canonicalize<T, Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static T Length<T>(this Interval<T, Closed, Closed> value)
        where T : struct, INumber<T>
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this IInterval<DateTime> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this Interval<DateTime, Closed, Closed> value)
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this IInterval<DateTimeOffset> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this Interval<DateTimeOffset, Closed, Closed> value)
        => Length(value, (left, right) => left - right);


    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The length of the interval</returns>
    public static int Length(this IInterval<DateOnly> value, int step = 1)
    {
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static int Length(this Interval<DateOnly, Closed, Closed> value)
        => Length(value, (left, right) => left.DayNumber - right.DayNumber);


    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this IInterval<TimeOnly> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan Length(this Interval<TimeOnly, Closed, Closed> value) => Length(value, (left, right) => left - right);

    private static TResult Length<T, TResult>(Interval<T, Closed, Closed> value, Func<T, T, TResult> length)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (value.IsEmpty)
        {
            return default;
        }
        return length(value.End, value.Start);
    }
}
