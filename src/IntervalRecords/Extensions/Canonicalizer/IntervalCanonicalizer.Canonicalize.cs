using Unbounded;

namespace IntervalRecords.Extensions;
public static partial class IntervalCanonicalizer
{
    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<int> Canonicalize(this Interval<int> source, IntervalType intervalType, int step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<double> Canonicalize(this Interval<double> source, IntervalType intervalType, double step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, IntervalType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, IntervalType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, IntervalType intervalType, int step)
        => Canonicalize(source, intervalType, b => b.AddDays(step), b => b.AddDays(-step));

    /// <summary>
    /// Canonicalizes the given interval by transforming it into the given interval type.
    /// </summary>
    /// <param name="source">The source interval to be canonicalized.</param>
    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
    /// <returns>The canonicalized interval in the desired interval type.</returns>
    public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, IntervalType intervalType, TimeSpan step)
        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Add(-step));

    private static Interval<T> Canonicalize<T>(
        Interval<T> source,
        IntervalType intervalType,
        Func<Unbounded<T>, Unbounded<T>> add,
        Func<Unbounded<T>, Unbounded<T>> substract)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => intervalType switch
        {
            IntervalType.Closed => ToClosed(source, add, substract),
            IntervalType.ClosedOpen => ToClosedOpen(source, add),
            IntervalType.OpenClosed => ToOpenClosed(source, substract),
            IntervalType.Open => ToOpen(source, add, substract),
            _ => throw new NotImplementedException()
        };

    private static ClosedOpenInterval<T> ToClosedOpen<T>(
        Interval<T> source,
        Func<Unbounded<T>, Unbounded<T>> add)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (source.IsEmpty || source.GetIntervalType() == IntervalType.ClosedOpen)
        {
            return new ClosedOpenInterval<T>(source.Start, source.End);
        }
        return new ClosedOpenInterval<T>(
            source.StartInclusive ? source.Start : add(source.Start),
            source.EndInclusive ? add(source.End) : source.End);
    }

    private static OpenClosedInterval<T> ToOpenClosed<T>(Interval<T> source, Func<Unbounded<T>, Unbounded<T>> substract)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (source.IsEmpty || source.GetIntervalType() == IntervalType.OpenClosed)
        {
            return new OpenClosedInterval<T>(source.Start, source.End);
        }
        return new OpenClosedInterval<T>(
            source.StartInclusive ? substract(source.Start) : source.Start,
            source.EndInclusive ? source.End : substract(source.End));
    }
}
