using Intervals.Bounds;
using Intervals.Types;
using System.Numerics;

namespace Intervals.Operations;
public static class LeftBoundedCanonicalizeExtensions
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedLeftBoundedInterval<T, L> Canonicalize<T, L>(this ILeftBoundedInterval<T> value, T? step = null)
        where T : struct, INumber<T>
        where L : struct, IBound
    {
        step ??= T.One;
        return Canonicalize<T, L>(value, n => n + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedLeftBoundedInterval<DateTime, L> Canonicalize<L>(this ILeftBoundedInterval<DateTime> source, TimeSpan? step = null)
        where L : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTime, L>(source, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedLeftBoundedInterval<DateTimeOffset, L> Canonicalize<L>(this ILeftBoundedInterval<DateTimeOffset> source, TimeSpan? step = null)
        where L : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<DateTimeOffset, L>(source, b => b + step.Value, b => b - step.Value);
    }

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedLeftBoundedInterval<DateOnly, L> Canonicalize<L>(this ILeftBoundedInterval<DateOnly> source, int step = 1)
        where L : struct, IBound
    {
        return Canonicalize<DateOnly, L>(source, b => b.AddDays(step), b => b.AddDays(-step));
    }


    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static TypedLeftBoundedInterval<TimeOnly, L> Canonicalize<L>(this ILeftBoundedInterval<TimeOnly> source, TimeSpan? step = null)
        where L : struct, IBound
    {
        step ??= TimeSpan.FromSeconds(1);
        return Canonicalize<TimeOnly, L>(source, b => b.Add(step.Value), b => b.Add(-step.Value));
    }

    private static TypedLeftBoundedInterval<T, L> Canonicalize<T, L>(
        ILeftBoundedInterval<T> source,
        Func<T, T> add,
        Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
    {
        return (L.Bound) switch
        {
            Bound.Closed => ToClosed<T, L>(source, add),
            Bound.Open => ToOpen<T, L>(source, substract),
            _ => throw new NotImplementedException()
        };
    }

    private static TypedLeftBoundedInterval<T, L> ToClosed<T, L>(
        ILeftBoundedInterval<T> value,
        Func<T, T> add)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
    {
        return new LeftBoundedInterval<T, L>(value.StartBound.IsClosed() ? value.Start : add(value.Start));
    }

    private static TypedLeftBoundedInterval<T, L> ToOpen<T, L>(ILeftBoundedInterval<T> value, Func<T, T> substract)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
    {
        return new LeftBoundedInterval<T, L>(value.StartBound.IsOpen() ? value.Start : substract(value.Start));
    }
}
