using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<T> Canonicalize<T>(this Interval<T> source, BoundaryType intervalType, T step)
        where T : struct, INumber<T>
        => Canonicalize(source, intervalType, n => n + step, b => b - step);

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, BoundaryType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b + step, b => b - step);

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, BoundaryType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b + step, b => b - step);

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, BoundaryType intervalType, int step)
        => Canonicalize(source, intervalType, b => b.AddDays(step), b => b.AddDays(-step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, BoundaryType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Add(-step));

    private static Interval<T> Canonicalize<T>(
        Interval<T> source,
        BoundaryType intervalType,
        Func<T, T> add,
        Func<T, T> substract)
        where T : struct, IComparable<T>, ISpanParsable<T>
        => intervalType switch
        {
            BoundaryType.Closed => ToClosed(source, add, substract),
            BoundaryType.ClosedOpen => ToClosedOpen(source, add),
            BoundaryType.OpenClosed => ToOpenClosed(source, substract),
            BoundaryType.Open => ToOpen(source, add, substract),
            _ => throw new NotImplementedException()
        };

    private static Interval<T> ToClosedOpen<T>(
        Interval<T> source,
        Func<T, T> add)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (source.IsEmpty || source.GetBoundaryType() == BoundaryType.ClosedOpen)
        {
            return new Interval<T>(source.Start, source.End, true, false);
        }
        return new Interval<T>(
            !source.StartInclusive && source.Start.HasValue ? add(source.Start.Value) : source.Start,
            source.EndInclusive && source.End.HasValue ? add(source.End.Value) : source.End,
            true,
            false);
    }

    private static Interval<T> ToOpenClosed<T>(Interval<T> source, Func<T, T> substract)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (source.IsEmpty || source.GetBoundaryType() == BoundaryType.OpenClosed)
        {
            return new Interval<T>(source.Start, source.End, false, true);
        }
        return new Interval<T>(
            source.StartInclusive && source.Start.HasValue ? substract(source.Start.Value) : source.Start,
            !source.EndInclusive && source.End.HasValue ? substract(source.End.Value) : source.End,
            false,
            true);
    }
}
