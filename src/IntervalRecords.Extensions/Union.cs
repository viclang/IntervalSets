namespace IntervalRecords.Extensions
{
    public static partial class IntervalExtensions
    {
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
