using System.Diagnostics.Contracts;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Returns an enumerable of results obtained by applying a specified function to the adjacent intervals in the source.
        /// The result of the function applied to the intervals must be a struct and can be null.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <typeparam name="TResult">The type of the result of the function applied to the intervals.</typeparam>
        /// <param name="source">The collection of intervals.</param>
        /// <param name="resultSelector">The function used to combine adjacent intervals.</param>
        /// <returns>The pairwise result sequence of intervals.</returns>
        [Pure]
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> source,
            Func<Interval<T>, Interval<T>, TResult?> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = source.OrderBy(x => x.Start).GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (result != null)
                    yield return result;

                previous = e.Current;
            }
        }
    }
}
