namespace IntervalRecords.Extensions
{
    public static partial class IntervalExtensions
    {
        /// <summary>
        /// Calculates the intersect of two intervals if they overlap.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval</param>
        /// <param name="second">The second interval</param>
        /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
        public static Interval<T>? Intersect<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.IsConnected(second)
                ? GetIntersectValue(first, second)
                : null;

        /// <summary>
        /// Calculates the intersect of two intervals if they overlap, otherwise returns an empty interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval</param>
        /// <param name="second">The second interval</param>
        /// <returns>The intersect of the two intervals if they overlap, otherwise returns an empty interval.</returns>
        public static Interval<T> IntersectOrEmpty<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.IsConnected(second)
                ? GetIntersectValue(first, second)
                : Interval<T>.Empty(first.IntervalType);

        /// <summary>
        /// Returns an enumeration of intervals that represent the intersection of all overlapping intervals in the input sequence.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="source">The collection of intervals.</param>
        /// <returns>An enumeration of intervals that represent the intersection of all overlapping intervals in the input sequence.</returns>
        public static IEnumerable<Interval<T>> IntersectAll<T>(
            this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Pairwise((a, b) => a.Intersect(b)).Where(i => !i.IsEmpty);

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

            return Interval.Create(maxByStart.Start, minByEnd.End, startInclusive, endInclusive);
        }
    }
}
