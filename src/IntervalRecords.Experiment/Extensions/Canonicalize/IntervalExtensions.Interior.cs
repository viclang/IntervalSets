using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<T> Interior<T>(this Interval<T> source, T step) where T : struct, INumber<T>
        => ToOpen(source, end => end + step, start => start - step);

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateTime> Interior(this Interval<DateTime> source, TimeSpan step)
        => ToOpen(source, end => end + step, start => start - step);

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan step)
        => ToOpen(source, end => end + step, start => start - step);

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<DateOnly> Interior(this Interval<DateOnly> source, int step)
        => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));

    /// <summary>
    /// Converts an interval to an open interval.
    /// </summary>
    /// <param name="source">The interval to be converted.</param>
    /// <returns>A open interval that is equivalent to `source`.</returns>
    public static Interval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan step)
        => ToOpen(source, end => end.Add(step), start => start.Add(-step));

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
