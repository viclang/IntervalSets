//using System.Numerics;

//namespace IntervalRecords.Experiment.Extensions;
//public static partial class IntervalExtensions
//{
//    /// <summary>
//    /// Converts an interval to an open interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A open interval that is equivalent to `source`.</returns>
//    public static OpenInterval<T> Interior<T>(this Interval<T> source, T step)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>, IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>
//        => ToOpen(source, end => end.Add(step), start => start.Substract(step));

//    /// <summary>
//    /// Converts an interval to an open interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A open interval that is equivalent to `source`.</returns>
//    public static OpenInterval<DateTime> Interior(this Interval<DateTime> source, TimeSpan step)
//        => ToOpen(source, end => end.Add(step), start => start.Substract(step));

//    /// <summary>
//    /// Converts an interval to an open interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A open interval that is equivalent to `source`.</returns>
//    public static OpenInterval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan step)
//        => ToOpen(source, end => end.Add(step), start => start.Substract(step));

//    /// <summary>
//    /// Converts an interval to an open interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A open interval that is equivalent to `source`.</returns>
//    public static OpenInterval<DateOnly> Interior(this Interval<DateOnly> source, int step)
//        => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));

//    /// <summary>
//    /// Converts an interval to an open interval.
//    /// </summary>
//    /// <param name="source">The interval to be converted.</param>
//    /// <returns>A open interval that is equivalent to `source`.</returns>
//    public static OpenInterval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan step)
//        => ToOpen(source, end => end.Add(step), start => start.Add(-step));

//    private static OpenInterval<T> ToOpen<T>(Interval<T> source, Func<Unbounded<T>, Unbounded<T>> add, Func<Unbounded<T>, Unbounded<T>> substract)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
//    {
//        if (source.IsEmpty || !source.StartInclusive && !source.EndInclusive)
//        {
//            return new OpenInterval<T>(source.Start, source.End);
//        }
//        return new OpenInterval<T>(
//            source.StartInclusive ? substract(source.Start) : source.Start,
//            source.EndInclusive ? add(source.End) : source.End);
//    }
//}
