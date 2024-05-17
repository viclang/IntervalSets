using IntervalSet.Types;
using IntervalSet.Types.Unbounded;
using System.Numerics;

namespace IntervalSet.Operations;
public static class RightBoundedCanonicalizeExtensions
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedRightBoundedInterval<T, R> Canonicalize<T, R>(this IRightBoundedInterval<T> value, T? step = null)
        where T : struct, INumber<T>
        where R : struct, IBound
    {
        step ??= T.One;
        return Canonicalize<T, R>(value, n => n + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedRightBoundedInterval<DateTime, R> Canonicalize<R>(this IRightBoundedInterval<DateTime> source, TimeSpan? step = null)
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTime, R>(source, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedRightBoundedInterval<DateTimeOffset, R> Canonicalize<R>(this IRightBoundedInterval<DateTimeOffset> source, TimeSpan? step = null)
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTimeOffset, R>(source, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedRightBoundedInterval<DateOnly, R> Canonicalize<R>(this IRightBoundedInterval<DateOnly> source, int step = 1)
        where R : struct, IBound
    {
        return Canonicalize<DateOnly, R>(source, b => b.AddDays(step), b => b.AddDays(-step));
    }


    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedRightBoundedInterval<TimeOnly, R> Canonicalize<R>(this IRightBoundedInterval<TimeOnly> source, TimeSpan? step = null)
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<TimeOnly, R>(source, b => b.Add(step.Value), b => b.Add(-step.Value));
    }

    private static TypedRightBoundedInterval<T, R> Canonicalize<T, R>(
        IRightBoundedInterval<T> source,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
    {
        return (R.Bound) switch
        {
            Bound.Closed => ToClosed<T, R>(source, add),
            Bound.Open => ToOpen<T, R>(source, substract),
            _ => throw new NotImplementedException()
        };
    }

    private static TypedRightBoundedInterval<T, R> ToClosed<T, R>(
        IRightBoundedInterval<T> value,
        Func<T, T> add)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
    {
        return new RightBoundedInterval<T, R>(value.EndBound.IsClosed() ? value.End : add(value.End));
    }

    private static TypedRightBoundedInterval<T, R> ToOpen<T, R>(IRightBoundedInterval<T> value, Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
    {
        return new RightBoundedInterval<T, R>(value.EndBound.IsOpen() ? value.End : substract(value.End));
    }
}

