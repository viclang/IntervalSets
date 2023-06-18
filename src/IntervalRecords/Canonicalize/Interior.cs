using Unbounded;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<int> Interior(this Interval<int> source, int step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));

        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<double> Interior(this Interval<double> source, double step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));

        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<DateTime> Interior(this Interval<DateTime> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));

        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Substract(step));

        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<DateOnly> Interior(this Interval<DateOnly> source, int step)
            => ToOpen(source, end => end.AddDays(step), start => start.AddDays(-step));

        /// <summary>
        /// Converts an interval to an open interval.
        /// </summary>
        /// <param name="source">The interval to be converted.</param>
        /// <returns>A open interval that is equivalent to `source`.</returns>
        public static Interval<TimeOnly> Interior(this Interval<TimeOnly> source, TimeSpan step)
            => ToOpen(source, end => end.Add(step), start => start.Add(-step));

        private static Interval<T> ToOpen<T>(Interval<T> source, Func<Unbounded<T>, Unbounded<T>> add, Func<Unbounded<T>, Unbounded<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || !source.StartInclusive && !source.EndInclusive)
            {
                return source;
            }
            return new OpenInterval<T>(
                source.StartInclusive ? substract(source.Start) : source.Start,
                source.EndInclusive ? add(source.End) : source.End);
        }
    }
}
