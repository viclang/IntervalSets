namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Computes the union of two intervals if they overlap.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval to be unioned.</param>
        /// <param name="second">The second interval to be unioned.</param>
        /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
        public static Interval<T>? Union<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.IsConnected(second)
                ? Hull(first, second)
                : null;

        /// <summary>
        /// Computes the union of two intervals if they overlap, otherwise returns an empty interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval to be unioned.</param>
        /// <param name="second">The second interval to be unioned.</param>
        /// <returns>The union of the two intervals if they overlap, otherwise returns an empty interval.</returns>
        public static Interval<T> UnionOrEmpty<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first.IsConnected(second)
                ? Hull(first, second)
                : Interval<T>.Empty(first.IntervalType);

        /// <summary>
        /// Computes the union of a collection of intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="source">The collection of intervals.</param>
        /// <returns>The union of the collection of intervals.</returns>
        public static IEnumerable<Interval<T>> UnionAll<T>(this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Reduce((a, b) => a.Union(b));
    }
}
