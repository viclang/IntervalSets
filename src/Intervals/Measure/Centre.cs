using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static double? Centre(this Interval<int> source)
            => Centre(source, (end, start) => ((double)end + start) / 2);

        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static double? Centre(this Interval<double> source)
            => Centre(source, (end, start) => (end + start) / 2);

        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static DateTime? Centre(this Interval<DateTime> source)
            => Centre(source, (end, start) => start.Add((end - start) / 2));

        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> source)
            => Centre(source, (end, start) => start.Add((end - start) / 2));

        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static DateOnly? Centre(this Interval<DateOnly> source)
            => Centre(source, (end, start) => start.AddDays((end.DayNumber - start.DayNumber) / 2));

        /// <summary>
        /// Calculates the centre of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the centre of</param>
        /// <returns>The centre of the interval</returns>
        [Pure]
        public static TimeOnly? Centre(this Interval<TimeOnly> source)
            => Centre(source, (end, start) => start.Add((end - start) / 2));


        private static TResult? Centre<T, TResult>(Interval<T> source, Func<T, T, TResult> centre)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return centre(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
        }
    }
}
