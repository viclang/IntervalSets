using IntervalSet.Bounds;
using IntervalSet.Types;
using System.Numerics;

namespace IntervalSet.Operations;
public static class AbstractCanonicalizeExtensions
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static IAbstractInterval<T> Canonicalize<T, L, R>(this IAbstractInterval<T> value, T? step = null)
        where T : struct, INumber<T>
        where L : struct, IBound
        where R : struct, IBound
    => value switch
    {
        IBoundedInterval<T> boundedInterval => boundedInterval.Canonicalize<T, L, R>(step),
        IComplementInterval<T> complementInterval => complementInterval.Canonicalize<T, L, R>(step),
        ILeftBoundedInterval<T> leftBoundedInterval => leftBoundedInterval.Canonicalize<T, L>(step),
        IRightBoundedInterval<T> rightBoundedInterval => rightBoundedInterval.Canonicalize<T, R>(step),
        _ => throw new NotImplementedException(),
    };

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static IAbstractInterval<DateTime> Canonicalize<L, R>(this IAbstractInterval<DateTime> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    => value switch
    {
        IBoundedInterval<DateTime> boundedInterval => boundedInterval.Canonicalize<L, R>(step),
        IComplementInterval<DateTime> complementInterval => complementInterval.Canonicalize<L, R>(step),
        ILeftBoundedInterval<DateTime> leftBoundedInterval => leftBoundedInterval.Canonicalize<L>(step),
        IRightBoundedInterval<DateTime> rightBoundedInterval => rightBoundedInterval.Canonicalize<R>(step),
        _ => throw new NotImplementedException(),
    };

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static IAbstractInterval<DateTimeOffset> Canonicalize<L, R>(this IAbstractInterval<DateTimeOffset> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    => value switch
    {
        IBoundedInterval<DateTimeOffset> boundedInterval => boundedInterval.Canonicalize<L, R>(step),
        IComplementInterval<DateTimeOffset> complementInterval => complementInterval.Canonicalize<L, R>(step),
        ILeftBoundedInterval<DateTimeOffset> leftBoundedInterval => leftBoundedInterval.Canonicalize<L>(step),
        IRightBoundedInterval<DateTimeOffset> rightBoundedInterval => rightBoundedInterval.Canonicalize<R>(step),
        _ => throw new NotImplementedException(),
    };

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static IAbstractInterval<DateOnly> Canonicalize<L, R>(this IAbstractInterval<DateOnly> value, int step = 1)
        where L : struct, IBound
        where R : struct, IBound
     => value switch
     {
         IBoundedInterval<DateOnly> boundedInterval => boundedInterval.Canonicalize<L, R>(step),
         IComplementInterval<DateOnly> complementInterval => complementInterval.Canonicalize<L, R>(step),
         ILeftBoundedInterval<DateOnly> leftBoundedInterval => leftBoundedInterval.Canonicalize<L>(step),
         IRightBoundedInterval<DateOnly> rightBoundedInterval => rightBoundedInterval.Canonicalize<R>(step),
         _ => throw new NotImplementedException(),
     };


    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="value">The interval to be canonicalized.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static IAbstractInterval<TimeOnly> Canonicalize<L, R>(this IAbstractInterval<TimeOnly> value, TimeSpan? step = null)
        where L : struct, IBound
        where R : struct, IBound
    => value switch
    {
        IBoundedInterval<TimeOnly> boundedInterval => boundedInterval.Canonicalize<L, R>(step),
        IComplementInterval<TimeOnly> complementInterval => complementInterval.Canonicalize<L, R>(step),
        ILeftBoundedInterval<TimeOnly> leftBoundedInterval => leftBoundedInterval.Canonicalize<L>(step),
        IRightBoundedInterval<TimeOnly> rightBoundedInterval => rightBoundedInterval.Canonicalize<R>(step),
        _ => throw new NotImplementedException(),
    };
}
