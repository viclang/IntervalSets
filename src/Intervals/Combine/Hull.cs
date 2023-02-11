using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Computes the smallest interval that contains both input intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds. Must be a struct that implements IEquatable<T>, IComparable<T>, and IComparable.</typeparam>
        /// <param name="first">The first interval to compute the hull of.</param>
        /// <param name="second">The second interval to compute the hull of.</param>
        /// <returns>The smallest interval that contains both input intervals.</returns>
        [Pure]
        public static Interval<T> Hull<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(first, second, i => i.Start);
            var maxByEnd = MaxBy(first, second, i => i.End);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive || second.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive || second.EndInclusive
                : maxByEnd.EndInclusive;

            return new Interval<T>(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
        }

        /// <summary>
        /// Computes the smallest interval that contains all input intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds. Must be a struct that implements IEquatable<T>, IComparable<T>, and IComparable.</typeparam>
        /// <param name="source">The intervals to compute the hull of.</param>
        /// <returns>The smallest interval that contains all input intervals, or null if the input is empty.</returns>
        [Pure]
        public static Interval<T>? Hull<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!source.Any())
            {
                return null;
            }
            var min = source.MinBy(i => i.Start);
            var max = source.MaxBy(i => i.End);

            return new Interval<T>(
                min.Start,
                max.End,
                min.StartInclusive,
                max.EndInclusive);
        }
    }
}
