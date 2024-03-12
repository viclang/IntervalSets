using System.Numerics;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static T? Centre<T>(this Interval<T> value)
        where T : struct, INumber<T>
        => Centre(value, (end, start) => (end + start) / (T.One + T.One));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateTime? Centre(this Interval<DateTime> value)
        => Centre(value, (end, start) => start + ((end - start) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value)
        => Centre(value, (end, start) => start + ((end - start) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static DateOnly? Centre(this Interval<DateOnly> value)
        => Centre(value, (end, start) => start.AddDays((end.DayNumber - start.DayNumber) / 2));

    /// <summary>
    /// Calculates the centre of the interval
    /// </summary>
    /// <param name="value">The interval to calculate the centre of</param>
    /// <returns>The centre of the interval</returns>
    public static TimeOnly? Centre(this Interval<TimeOnly> value)
        => Centre(value, (end, start) => start.Add((end - start) / 2));


    private static TResult? Centre<T, TResult>(Interval<T> value, Func<T, T, TResult> centre)
        where T : struct, IComparable<T>, ISpanParsable<T>
        where TResult : struct
    {
        if (value.Start is null || value.End is null || value.IsEmpty)
        {
            return null;
        }
        return centre(value.End!.Value, value.Start!.Value);
    }
}
