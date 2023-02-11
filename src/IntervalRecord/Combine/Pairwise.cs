using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        [Pure]
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> source,
            Func<Interval<T>, Interval<T>, TResult> resultSelector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = source.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                yield return resultSelector(previous, e.Current);
                previous = e.Current;
            }
        }

        /// <summary>
        /// Returns an enumerable of results obtained by applying a specified function to the adjacent intervals in the source.
        /// The result of the function applied to the intervals must be a struct and can be null.
        /// </summary>
        /// <typeparam name="T">The type of the interval.</typeparam>
        /// <typeparam name="TResult">The type of the result of the function applied to the intervals. Must be a struct.</typeparam>
        /// <param name="source">The source interval enumerable.</param>
        /// <param name="resultSelector">The function to apply to the adjacent intervals in the source.</param>
        /// <returns>An enumerable of results obtained by applying a specified function to the adjacent intervals in the source.</returns>
        [Pure]
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> source,
            Func<Interval<T>, Interval<T>, TResult?> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            using var e = source.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (result != null)
                    yield return result.Value;

                previous = e.Current;
            }
        }
    }
}
