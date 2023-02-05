using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class IntervalExtensions
    {
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

        public static IEnumerable<Interval<T>> Pairwise<T>(
            this IEnumerable<Interval<T>> values, Func<Interval<T>, Interval<T>, Interval<T>> resultSelector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

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
        public static IEnumerable<Interval<T>> Pairwise<T>(
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
                if (result != null)
                    yield return result.Value;

                previous = e.Current;
            }
        }

        [Pure]
        public static IEnumerable<Interval<T>> PairwiseFilter<T>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, Interval<T>> resultSelector,
            Func<Interval<T>, bool> predicate)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (predicate(result))
                    yield return result;

                previous = e.Current;
            }
        }

        [Pure]
        public static IEnumerable<Interval<T>> PairwiseFilter<T>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, Interval<T>?> resultSelector,
            Func<Interval<T>, bool> predicate)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);
                if (result != null && predicate(result.Value))
                    yield return result.Value;

                previous = e.Current;
            }
        }
    }
}
