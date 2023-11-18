namespace IntervalRecords.Linq
{
    public static partial class IntervalExtensions
    {
        /// <summary>
        /// Returns the sequence of intervals resulting from combining adjacent intervals in the collection of intervals
        /// using the specified result selector function.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="source">The collection of intervals.</param>
        /// <param name="resultSelector">The function used to combine adjacent intervals.</param>
        /// <returns>The reduced sequence of intervals.</returns>
        public static IEnumerable<T> Reduce<T>(
            this IOrderedEnumerable<T> source,
            Func<T, T, T?> resultSelector)
           
        {
            using var e = source.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (result is null)
                {
                    yield return previous;
                    previous = e.Current;
                }
                else
                {
                    previous = result;
                }
            }
            yield return previous;
        }
    }
}
