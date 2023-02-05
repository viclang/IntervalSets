using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
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
