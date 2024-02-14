//using IntervalRecords.Experiment.Endpoints;
//using System.Numerics;

//namespace IntervalRecords.Experiment.Extensions;
//public static partial class IntervalExtensions
//{
//    /// <summary>
//    /// Canonicalizes the given interval by transforming it into the given interval type.
//    /// </summary>
//    /// <param name="source">The source interval to be canonicalized.</param>
//    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
//    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
//    /// <returns>The canonicalized interval in the desired interval type.</returns>
//    public static Interval<T> Canonicalize<T>(this Interval<T> source, IntervalType intervalType, T step)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>, IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>
//        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

//    /// <summary>
//    /// Canonicalizes the given interval by transforming it into the given interval type.
//    /// </summary>
//    /// <param name="source">The source interval to be canonicalized.</param>
//    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
//    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
//    /// <returns>The canonicalized interval in the desired interval type.</returns>
//    public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, IntervalType intervalType, TimeSpan step)
//        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

//    /// <summary>
//    /// Canonicalizes the given interval by transforming it into the given interval type.
//    /// </summary>
//    /// <param name="source">The source interval to be canonicalized.</param>
//    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
//    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
//    /// <returns>The canonicalized interval in the desired interval type.</returns>
//    public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, IntervalType intervalType, TimeSpan step)
//        => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

//    /// <summary>
//    /// Canonicalizes the given interval by transforming it into the given interval type.
//    /// </summary>
//    /// <param name="source">The source interval to be canonicalized.</param>
//    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
//    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
//    /// <returns>The canonicalized interval in the desired interval type.</returns>
//    public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, IntervalType intervalType, int step)
//        => Canonicalize(source, intervalType, b => b.AddDays(step), b => b.AddDays(-step));

//    /// <summary>
//    /// Canonicalizes the given interval by transforming it into the given interval type.
//    /// </summary>
//    /// <param name="source">The source interval to be canonicalized.</param>
//    /// <param name="intervalType">The desired interval type to transform the source interval into.</param>
//    /// <param name="step">The step value used to increment or decrement the interval bounds.</param>
//    /// <returns>The canonicalized interval in the desired interval type.</returns>
//    public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, IntervalType intervalType, TimeSpan step)
//        => Canonicalize(source, intervalType, (b, step) => b.Add(step));

//    private static Interval<T> Canonicalize<T, TStep>(
//        Interval<T> source,
//        IntervalType intervalType,
//        Func<T, TStep, T> add)
//        where T : struct, INumber<T>
//        => intervalType switch
//        {
//            IntervalType.Closed => ToClosed(source, add(),
//            IntervalType.ClosedOpen => ToClosedOpen(source, add),
//            IntervalType.OpenClosed => ToOpenClosed(source, substract),
//            IntervalType.Open => ToOpen(source, add, substract),
//            _ => throw new NotImplementedException()
//        };

//    private static Interval<T> ToClosedOpen<T>(
//        Interval<T> source,
//        Func<Endpoint<T>, Endpoint<T>> add)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
//    {
//        if (source.IsEmpty || source.GetIntervalType() == IntervalType.ClosedOpen)
//        {
//            return new Interval<T>(source.Start, source.End);
//        }
//        return new Interval<T>(
//            source.Start.Inclusive ? source.Start : add(source.Start),
//            source.End.Inclusive ? add(source.End) : source.End);
//    }

//    private static Interval<T> ToOpenClosed<T>(Interval<T> source, Func<Endpoint<T>, Endpoint<T>> substract)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
//    {
//        if (source.IsEmpty || source.GetIntervalType() == IntervalType.OpenClosed)
//        {
//            return new Interval<T>(source.Start, source.End);
//        }
//        return new Interval<T>(
//            source.Start.Inclusive ? substract(source.Start) : source.Start,
//            source.End.Inclusive ? source.End : substract(source.End));
//    }
//}
