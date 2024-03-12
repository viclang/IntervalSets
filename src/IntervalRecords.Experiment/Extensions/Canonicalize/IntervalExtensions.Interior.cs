using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<T> Interior<T>(this Interval<T> source, T? step = null) where T : struct, INumber<T>
    {
        step = step ?? T.One;
        return ToOpen(source, end => end + step.Value, start => start - step.Value);
    }

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateTime> Interior(this Interval<DateTime> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToOpen(source, end => end + step.Value, start => start - step.Value);
    }

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToOpen(source, end => end + step.Value, start => start - step.Value);
    }

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 day.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateOnly> Interior(this Interval<DateOnly> source, int step = 1)
        => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds. Defaults to 1 second.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan? step = null)
    {
        step ??= TimeSpan.FromSeconds(1);
        return ToOpen(source, end => end.Add(step.Value), start => start.Add(-step.Value));
    }

    private static Interval<T> ToOpen<T>(Interval<T> source, Func<T, T> add, Func<T, T> substract)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (source.IsEmpty || !source.StartInclusive && !source.EndInclusive)
        {
            return new Interval<T>(source.Start, source.End, false, false);
        }
        return new Interval<T>(
            source.StartInclusive && source.Start.HasValue ? substract(source.Start.Value) : source.Start,
            source.EndInclusive && source.End.HasValue ? add(source.End.Value) : source.End,
            false,
            false);
    }
}
