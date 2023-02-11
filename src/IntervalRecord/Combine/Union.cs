using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        /// <summary>
        /// Computes the union of two intervals if they overlap.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds</typeparam>
        /// <param name="first">The first interval to be unioned</param>
        /// <param name="second">The second interval to be unioned</param>
        /// <returns>The union of the two intervals if they overlap, otherwise returns null</returns>
        [Pure]
        public static Interval<T>? Union<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? null
                : Hull(first, second);

        /// <summary>
        /// Computes the union of two intervals if they overlap, otherwise returns the default value.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds</typeparam>
        /// <param name="first">The first interval to be unioned</param>
        /// <param name="second">The second interval to be unioned</param>
        /// <param name="defaultValue">The default value to be returned if the two intervals don't overlap</param>
        /// <returns>The union of the two intervals if they overlap, otherwise returns the default value</returns>
        [Pure]
        public static Interval<T> UnionOrDefault<T>(this Interval<T> first, Interval<T> second, Interval<T> defaultValue = default)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => !first.Overlaps(second, true)
                ? defaultValue
                : Hull(first, second);

        /// <summary>
        /// Computes the union of a collection of intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds</typeparam>
        /// <param name="source">The collection of intervals to be unioned</param>
        /// <returns>The union of the collection of intervals</returns>
        [Pure]
        public static IEnumerable<Interval<T>> Union<T>(this IEnumerable<Interval<T>> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.Reduce((a, b) => a.Union(b));
    }
}
