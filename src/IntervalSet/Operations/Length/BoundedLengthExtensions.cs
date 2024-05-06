using Intervals.Bounds;
using Intervals.Types;
using System.Numerics;

namespace Intervals.Operations;
public static partial class BoundedLengthExtensions
{

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The length of the interval</returns>
    public static T? Length<T>(this IBoundedInterval<T> value, T? step = null)
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
    public static T? Length<T>(this TypedBoundedInterval<T, Closed, Closed> value)
        where T : struct, INumber<T>
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this IBoundedInterval<DateTime> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this TypedBoundedInterval<DateTime, Closed, Closed> value)
        => Length(value, (left, right) => left - right);

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this IBoundedInterval<DateTimeOffset> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this TypedBoundedInterval<DateTimeOffset, Closed, Closed> value)
        => Length(value, (left, right) => left - right);


    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The length of the interval</returns>
    public static int? Length(this IBoundedInterval<DateOnly> value, int step = 1)
    {
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static int? Length(this TypedBoundedInterval<DateOnly, Closed, Closed> value)
        => Length(value, (left, right) => left.DayNumber - right.DayNumber);


    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this IBoundedInterval<TimeOnly> value, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return value.Canonicalize<Closed, Closed>(step).Length();
    }

    /// <summary>
    /// Calculates the length of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the length of</param>
    /// <returns>The length of the interval</returns>
    public static TimeSpan? Length(this TypedBoundedInterval<TimeOnly, Closed, Closed> value) => Length(value, (left, right) => left - right);

    private static TResult? Length<T, TResult>(TypedBoundedInterval<T, Closed, Closed> value, Func<T, T, TResult> length)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, ISpanParsable<TResult>
    {
        if (value.IsEmpty)
        {
            return default(TResult);
        }
        return length(value.End, value.Start);
    }
}
