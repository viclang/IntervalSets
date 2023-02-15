using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Returns the gap between two intervals, if any.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must be a struct, implement IEquatable, IComparable and IComparable.</typeparam>
        /// <param name="first">The first interval.</param>
        /// <param name="second">The second interval.</param>
        /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
        [Pure]
        public static Interval<T>? Gap<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.GetIntervalOverlapping(second, true) switch
            {
                IntervalOverlapping.Before => new Interval<T>(first.End, second.Start, !first.EndInclusive, !second.StartInclusive),
                IntervalOverlapping.After => new Interval<T>(second.End, first.Start, !second.EndInclusive, !first.StartInclusive),
                _ => null
            };

        /// <summary>
        /// Returns the gap between two intervals, or a default interval if the two intervals overlap.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must be a struct, implement IEquatable, IComparable and IComparable.</typeparam>
        /// <param name="first">The first interval.</param>
        /// <param name="second">The second interval.</param>
        /// <param name="defaultValue">The default interval to return if the two intervals overlap. The default value is default(Interval&lt;T&gt;).</param>
        /// <returns>The gap between the two intervals, or a default interval if the two intervals overlap.</returns>
        [Pure]
        public static Interval<T> GapOrEmpty<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.GetIntervalOverlapping(second, true) switch
            {
                IntervalOverlapping.Before => new Interval<T>(first.End, second.Start, !first.EndInclusive, !second.StartInclusive),
                IntervalOverlapping.After => new Interval<T>(second.End, first.Start, !second.EndInclusive, !first.StartInclusive),
                _ => Empty<T>()
            };

        /// <summary>
        /// Returns the complement of a set of intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must be a struct, implement IEquatable, IComparable and IComparable.</typeparam>
        /// <param name="source">The set of intervals to complement.</param>
        /// <returns>The complement of the set of intervals, represented as a sequence of intervals.</returns>
        [Pure]
        public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Gap(b));
    }
}
