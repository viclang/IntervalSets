using IntervalSet.Bounds;
using IntervalSet.Types;
using System.Numerics;

namespace IntervalSet.Operations;
public static class BoundedCanonicalizeExtensions
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<T, L, R> Canonicalize<T, L, R>(this IBoundedInterval<T> value, T? step = null)
        where T : struct, INumber<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        step ??= T.One;
        return Canonicalize<T, L, R>(value, n => n + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTime, L, R> Canonicalize<L, R>(this IBoundedInterval<DateTime> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTime, L, R>(value, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTimeOffset, L, R> Canonicalize<L, R>(this IBoundedInterval<DateTimeOffset> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTimeOffset, L, R>(value, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateOnly, L, R> Canonicalize<L, R>(this IBoundedInterval<DateOnly> value, int step = 1)
        where L : struct, IBound
        where R : struct, IBound
    {
        return Canonicalize<DateOnly, L, R>(value, b => b.AddDays(step), b => b.AddDays(-step));
    }


    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<TimeOnly, L, R> Canonicalize<L, R>(this IBoundedInterval<TimeOnly> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<TimeOnly, L, R>(value, b => b.Add(step.Value), b => b.Add(-step.Value));
    }

    private static Interval<T, L, R> Canonicalize<T, L, R>(
        IBoundedInterval<T> source,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return (L.Bound, R.Bound) switch
        {
            (Bound.Closed, Bound.Closed) => ToClosed<T, L, R>(source, add, substract),
            (Bound.Closed, Bound.Open) => ToClosedOpen<T, L, R>(source, add),
            (Bound.Open, Bound.Closed) => ToOpenClosed<T, L, R>(source, substract),
            (Bound.Open, Bound.Open) => ToOpen<T, L, R>(source, add, substract),
            _ => throw new NotImplementedException()
        };
    }

    private static Interval<T, L, R> ToClosed<T, L, R>(
        IBoundedInterval<T> value,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsClosed() ? value.Start : add(value.Start),
            value.EndBound.IsClosed() ? value.End : substract(value.End));
    }

    private static Interval<T, L, R> ToClosedOpen<T, L, R>(
       IBoundedInterval<T> value,
       Func<T, T> add)
       where T : notnull, IComparable<T>, ISpanParsable<T>
       where L : struct, IBound
       where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsClosed() ? value.Start : add(value.Start),
            value.EndBound.IsOpen() ? value.End : add(value.End));
    }

    private static Interval<T, L, R> ToOpenClosed<T, L, R>(IBoundedInterval<T> value, Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsOpen() ? value.Start : substract(value.Start),
            value.EndBound.IsClosed() ? value.End : substract(value.End));
    }

    private static Interval<T, L, R> ToOpen<T, L, R>(IBoundedInterval<T> value, Func<T, T> add, Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
        where R : struct, IBound
    {
        return new Interval<T, L, R>(
            value.StartBound.IsOpen() ? value.Start : substract(value.Start),
            value.EndBound.IsOpen() ? value.End : add(value.End));
    }
}
