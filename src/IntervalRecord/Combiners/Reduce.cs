using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static IEnumerable<Interval<T>> Reduce<T>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, Interval<T>?> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

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

        [Pure]
        public static IEnumerable<Interval<T>> Reduce<T>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, Interval<T>> resultSelector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

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
