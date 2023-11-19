using IntervalRecords.Extensions;
using System.Numerics;

namespace IntervalRecords.Extensions;
public static partial class IntervalCalculator
{
    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static T? Centre<T>(this Interval<T> source)
        where T : struct, INumber<T>
        => Centre(source, (end, start) => (end + start) / (T.One + T.One));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateTime? Centre(this Interval<DateTime> source)
        => Centre(source, (end, start) => start.Add((end - start) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateTimeOffset? Centre(this Interval<DateTimeOffset> source)
        => Centre(source, (end, start) => start.Add((end - start) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateOnly? Centre(this Interval<DateOnly> source)
        => Centre(source, (end, start) => start.AddDays((end.DayNumber - start.DayNumber) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="source">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static TimeOnly? Centre(this Interval<TimeOnly> source)
        => Centre(source, (end, start) => start.Add((end - start) / 2));


    private static TResult? Centre<T, TResult>(Interval<T> source, Func<T, T, TResult> centre)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct
    {
        if (source.GetBoundaryState() is not BoundaryState.Bounded || source.IsEmpty)
        {
            return null;
        }
        return centre(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
    }
}
