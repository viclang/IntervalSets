//using IntervalRecords.Experiment.Endpoints;
//using System.Net;
//using System.Numerics;

//namespace IntervalRecords.Experiment.Extensions;
//public static partial class IntervalExtensions
//{
//    /// <summary>
//    /// Converts an interval to a closed interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A closed interval that is equivalent to `source`.</returns>
//    public static Interval<T> Closure<T>(this Interval<T> source, T step)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>, INumber<T>
//        => ToClosed<T, T>(source, start => start.Add(step), end => end.Substract(step));

//    /// <summary>
//    /// Converts an interval to a closed interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A closed interval that is equivalent to `source`.</returns>
//    public static Interval<DateTime> Closure(this Interval<DateTime> source, TimeSpan step)
//        => ToClosed(source, start => start.Add(step), end => end.Add(-step));

//    /// <summary>
//    /// Converts an interval to a closed interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A closed interval that is equivalent to `source`.</returns>
//    public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> source, TimeSpan step)
//        => ToClosed(source, start => start.Add(step), end => end.Add(-step));

//    /// <summary>
//    /// Converts an interval to a closed interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A closed interval that is equivalent to `source`.</returns>
//    public static Interval<DateOnly> Closure(this Interval<DateOnly> source, int step)
//        => ToClosed(source, start => start.AddDays(step), end => end.AddDays(-step));

//    /// <summary>
//    /// Converts an interval to a closed interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A closed interval that is equivalent to `source`.</returns>
//    public static Interval<TimeOnly> Closure(this Interval<TimeOnly> source, TimeSpan step)
//        => ToClosed(source, start => start.Add(step), end => end.Add(-step));

//    private static Interval<T> ToClosed<T, TStep>(
//    Interval<T> source,
//        Func<Endpoint<T>, Endpoint<T>> add,
//        Func<Endpoint<T>, Endpoint<T>> substract)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
//    {
//        if (source.IsEmpty)
//        {
//            return Interval<T>.Empty;
//        }
//        if (source.GetIntervalType() == IntervalType.Closed)
//        {
//            return (Interval<T>)source;
//        }
//        return new Interval<T>(
//            source.Start.Inclusive ? source.Start : add(source.Start),
//            source.End.Inclusive ? source.End : substract(source.End));
//    }
//}
