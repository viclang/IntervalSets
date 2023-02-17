using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Computes the interval representing the portion of the first interval that does not overlap with the second interval.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="first">The first interval</param>
        /// <param name="second">The second interval</param>
        /// <returns>The portion of the first interval that does not overlap with the second interval, or null if the intervals do not overlap</returns>
        [Pure]
        public static Interval<T>? Except<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true) ? null : GetExceptValue(first, second);

        /// <summary>
        /// Computes the interval representing the portion of the first interval that does not overlap with the second interval, or an empty interval if they do not overlap.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="first">The first interval.</param>
        /// <param name="second">The second interval.</param>
        /// <returns>The portion of the first interval that does not overlap with the second interval, or an empty interval if they do not overlap.</returns>
        [Pure]
        public static Interval<T> ExceptOrEmpty<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true) ? Empty<T>() : GetExceptValue(first, second);

        /// <summary>
        /// Computes the collection of intervals representing the portions of the collection of intervals that do not overlap with each other.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="source">The collection of intervals.</param>
        /// <returns>The collection of intervals representing the portions of the collection of intervals that do not overlap with each other.</returns>
        [Pure]
        public static IEnumerable<Interval<T>> ExcludeOverlap<T>(this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Except(b)).Where(i => !i.IsEmpty());

        [Pure]
        private static Interval<T> GetExceptValue<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(first, second, i => i.Start);
            var maxByStart = MaxBy(first, second, i => i.Start);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive || second.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive || second.EndInclusive
                : maxByStart.EndInclusive;

            return first with { Start = minByStart.Start, End = maxByStart.Start, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
