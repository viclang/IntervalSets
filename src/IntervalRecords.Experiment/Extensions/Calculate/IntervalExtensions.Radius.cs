using IntervalRecords.Experiment.Endpoints;
using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the radius of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the radius of</param>
    /// <returns>The radius of the interval</returns>
    public static T? Radius<T, L, R>(this Interval<T, L, R> source)
        where T : struct, INumber<T>
        where L : IBound, new()
        where R : IBound, new()
        => Radius(source, (end, start) => (end - start) / (T.One + T.One));

    /// <summary>
    /// Calculates the radius of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the radius of</param>
    /// <returns>The radius of the interval</returns>
    public static TimeSpan? Radius<L, R>(this Interval<DateTime, L, R> source, Func<DateTime, TimeSpan, DateTime> func)
        where L : IBound, new()
        where R : IBound, new()
    {
        return Radius(source, (end, start) => (end - start) / 2);
    }

    /// <summary>
    /// Calculates the radius of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the radius of</param>
    /// <returns>The radius of the interval</returns>
    public static TimeSpan? Radius<L, R>(this Interval<DateTimeOffset, L, R> source)
        where L : IBound, new()
        where R : IBound, new()
    {
        return Radius(source, (end, start) => (end - start) / 2);
    }

    /// <summary>
    /// Calculates the radius of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the radius of</param>
    /// <returns>The radius of the interval</returns>
    public static int? Radius<L, R>(this Interval<DateOnly, L, R> source)
        where L : IBound, new()
        where R : IBound, new()
    {
        return Radius(source, (end, start) => (end.DayNumber - start.DayNumber) / 2);
    }

    /// <summary>
    /// Calculates the radius of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the radius of</param>
    /// <returns>The radius of the interval</returns>
    public static TimeSpan? Radius<L, R>(this Interval<TimeOnly, L, R> source)
        where L : IBound, new()
        where R : IBound, new()
    {
        return Radius(source, (end, start) => (end - start) / 2);
    }

    private static TResult? Radius<T, L, R, TResult>(Interval<T, L, R> value, Func<T, T, TResult> radius)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where L : IBound, new()
        where R : IBound, new()
        where TResult : struct
    {
        if (value.Start is null || value.End is null || value.IsEmpty)
        {
            return null;
        }
        return radius(value.End.GetValueOrDefault(), value.Start.GetValueOrDefault());
    }
}
