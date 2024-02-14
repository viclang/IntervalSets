//using System.Numerics;

//namespace IntervalRecords.Experiment.Extensions;
//public static partial class IntervalExtensions
//{
//    /// <summary>
//    /// Calculates the radius of the interval
//    /// </summary>
//    /// <param name="source">The interval to calculate the radius of</param>
//    /// <returns>The radius of the interval</returns>
//    public static T? Radius<T>(this Interval<T> source)
//        where T : struct, INumber<T>
//        => Radius(source, (end, start) => (end - start) / (T.One + T.One));

//    /// <summary>
//    /// Calculates the radius of the interval
//    /// </summary>
//    /// <param name="source">The interval to calculate the radius of</param>
//    /// <returns>The radius of the interval</returns>
//    public static TimeSpan? Radius(this Interval<DateTime> source, Func<DateTime, TimeSpan, DateTime> func)
//    {
//        return Radius(source, (end, start) => (end - start) / 2);
//    }

//    /// <summary>
//    /// Calculates the radius of the interval
//    /// </summary>
//    /// <param name="source">The interval to calculate the radius of</param>
//    /// <returns>The radius of the interval</returns>
//    public static TimeSpan? Radius(this Interval<DateTimeOffset> source)
//        => Radius(source, (end, start) => (end - start) / 2);

//    /// <summary>
//    /// Calculates the radius of the interval
//    /// </summary>
//    /// <param name="source">The interval to calculate the radius of</param>
//    /// <returns>The radius of the interval</returns>
//    public static int? Radius(this Interval<DateOnly> source)
//        => Radius(source, (end, start) => (end.DayNumber - start.DayNumber) / 2);

//    /// <summary>
//    /// Calculates the radius of the interval
//    /// </summary>
//    /// <param name="source">The interval to calculate the radius of</param>
//    /// <returns>The radius of the interval</returns>
//    public static TimeSpan? Radius(this Interval<TimeOnly> source)
//        => Radius(source, (end, start) => (end - start) / 2);

//    private static TResult? Radius<T, TResult>(Interval<T> source, Func<T, T, TResult> radius)
//        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
//        where TResult : struct
//    {
//        if (source.State is not IntervalState.Bounded || source.IsEmpty)
//        {
//            return null;
//        }
//        return radius(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
//    }
//}
