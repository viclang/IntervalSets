using System.Diagnostics.Contracts;

namespace Intervals
{
    public static partial class Interval
    {
        /// <summary>
        /// Returns the sequence of intervals resulting from combining adjacent intervals in the source
        /// using the specified result selector function. If the result selector function returns null,
        /// the two intervals are considered not to be mergeable, and are returned as separate intervals.
        /// </summary>
        /// <typeparam name="T">The type of the values in the intervals</typeparam>
        /// <param name="source">The source sequence of intervals</param>
        /// <param name="resultSelector">The function used to combine adjacent intervals</param>
        /// <returns>The reduced sequence of intervals</returns>
        [Pure]
        public static IEnumerable<Interval<T>> Reduce<T>(
            this IEnumerable<Interval<T>> source,
            Func<Interval<T>, Interval<T>, Interval<T>?> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = source.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (result == null)
                {
                    yield return previous;
                    previous = e.Current;
                }
                else
                {
                    previous = result.Value;
                }
            }
            yield return previous;
        }

        /// <summary>
        /// Returns the sequence of intervals resulting from combining adjacent intervals in the source
        /// using the specified result selector function.
        /// </summary>
        /// <typeparam name="T">The type of the values in the intervals</typeparam>
        /// <param name="source">The source sequence of intervals</param>
        /// <param name="resultSelector">The function used to combine adjacent intervals</param>
        /// <returns>The reduced sequence of intervals</returns>
        [Pure]
        public static IEnumerable<Interval<T>> Reduce<T>(
            this IEnumerable<Interval<T>> source,
            Func<Interval<T>, Interval<T>, Interval<T>> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = source.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                previous = resultSelector(previous, e.Current);
                yield return previous;
            }
        }
    }
}
