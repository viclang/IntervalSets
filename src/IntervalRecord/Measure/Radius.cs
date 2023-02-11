using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static double? Radius(this Interval<int> source)
        => Radius(source, (end, start) => ((double)end - start) / 2);

        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static double? Radius(this Interval<double> source)
            => Radius(source, (end, start) => (end - start) / 2);

        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> source)
            => Radius(source, (end, start) => (end - start) / 2);

        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static TimeSpan? Radius(this Interval<DateTimeOffset> source)
            => Radius(source, (end, start) => (end - start) / 2);

        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static int? Radius(this Interval<DateOnly> source)
            => Radius(source, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        /// <summary>
        /// Calculates the radius of the interval
        /// </summary>
        /// <param name="source">The interval to calculate the radius of</param>
        /// <returns>The radius of the interval</returns>
        [Pure]
        public static TimeSpan? Radius(this Interval<TimeOnly> source)
            => Radius(source, (end, start) => (end - start) / 2);

        private static TResult? Radius<T, TResult>(Interval<T> source, Func<T, T, TResult> radius)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return radius(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
        }
    }
}
