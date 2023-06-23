using IntervalRecords.Types;
using Unbounded;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<int> Closure(this Interval<int> source, int step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<double> Closure(this Interval<double> source, double step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<DateTime> Closure(this Interval<DateTime> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<DateTimeOffset> Closure(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<DateOnly> Closure(this Interval<DateOnly> source, int step)
            => ToClosed(source, start => start.AddDays(step), end => end.AddDays(-step));

        /// <summary>
        /// Converts an interval to a closed interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A closed interval that is equivalent to `source`.</returns>
        public static ClosedInterval<TimeOnly> Closure(this Interval<TimeOnly> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        private static ClosedInterval<T> ToClosed<T>(
            Interval<T> source,
            Func<Unbounded<T>, Unbounded<T>> add,
            Func<Unbounded<T>, Unbounded<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty)
            {
                return new ClosedInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN);
            }
            if (source.GetIntervalType() == IntervalType.Closed)
            {
                return (ClosedInterval<T>)source;
            }
            return new ClosedInterval<T>(
                source.StartInclusive ? source.Start : add(source.Start),
                source.EndInclusive ? source.End : substract(source.End));
        }
    }
}
