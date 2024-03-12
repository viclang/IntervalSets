using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Converts an interval to a closed interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>A closed interval that is equivalent to `source`.</returns>
    public static Interval<T> Closure<T>(this Interval<T> source, T? step = null)
        where T : struct, INumber<T>
    {
        step = step ?? T.One;
        return ToClosed(source, start => start + step.Value, end => end - step.Value);
    }

    /// <summary>
    /// Converts an interval to a closed interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A closed interval that is equivalent to `source`.</returns>
    public static Interval<DateTime> Closure(this Interval<DateTime> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToClosed(source, start => start.Add(step.Value), end => end.Add(-step.Value));
    }

    /// <summary>
    /// Converts an interval to a closed interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A closed interval that is equivalent to `source`.</returns>
    public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToClosed(source, start => start.Add(step.Value), end => end.Add(-step.Value));
    }

    /// <summary>
    /// Converts an interval to a closed interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>A closed interval that is equivalent to `source`.</returns>
    public static Interval<DateOnly> Closure(this Interval<DateOnly> source, int step = 1)
        => ToClosed(source, start => start.AddDays(step), end => end.AddDays(-step));

    /// <summary>
    /// Converts an interval to a closed interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A closed interval that is equivalent to `source`.</returns>
    public static Interval<TimeOnly> Closure(this Interval<TimeOnly> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToClosed(source, start => start.Add(step.Value), end => end.Add(-step.Value));
    }

    private static Interval<T> ToClosed<T>(
    Interval<T> source,
        Func<T, T> add,
        Func<T, T> substract)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (source.IsEmpty)
        {
            return Interval<T>.Empty;
        }
        if (source.GetBoundaryType() == BoundaryType.Closed)
        {
            return source;
        }
        return new Interval<T>(
            !source.StartInclusive && source.Start.HasValue ? add(source.Start.Value) : source.Start,
            !source.EndInclusive && source.End.HasValue ? substract(source.End.Value) : source.End,
            true,
            true);
    }
}
