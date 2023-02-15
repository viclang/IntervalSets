using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Calculates the intersect of two intervals
        /// </summary>
        /// <typeparam name="T">The type of the interval's endpoints</typeparam>
        /// <param name="first">The first interval</param>
        /// <param name="second">The second interval</param>
        /// <returns>The intersect of the two intervals or null if they don't overlap</returns>
        [Pure]
        public static Interval<T>? Intersect<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? null
                : GetIntersectValue(first, second);

        /// <summary>
        /// Calculates the intersect of two intervals
        /// </summary>
        /// <typeparam name="T">The type of the interval's endpoints</typeparam>
        /// <param name="first">The first interval</param>
        /// <param name="second">The second interval</param>
        /// <returns>The intersect of the two intervals or the default value if they don't overlap</returns>
        [Pure]
        public static Interval<T> IntersectOrEmpty<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? Empty<T>()
                : GetIntersectValue(first, second);

        [Pure]
        public static IEnumerable<Interval<T>> IntersectAll<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Intersect(b)).Where(i => !i.IsEmpty());

        [Pure]
        private static Interval<T> GetIntersectValue<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var maxByStart = MaxBy(first, second, i => i.Start);
            var minByEnd = MinBy(first, second, i => i.End);

            var startInclusive = first.Start == second.Start
                ? first.StartInclusive && second.StartInclusive
                : maxByStart.StartInclusive;

            var endInclusive = first.End == second.End
                ? first.EndInclusive && second.EndInclusive
                : minByEnd.EndInclusive;

            return first with { Start = maxByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }
    }
}
