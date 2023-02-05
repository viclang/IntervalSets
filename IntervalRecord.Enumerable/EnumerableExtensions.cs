using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static class EnumerableExtensions
    {
        [Pure]
        public static Interval<T>? Hull<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!values.Any())
            {
                return null;
            }
            var min = values.MinBy(x => x.Start);
            var max = values.MaxBy(x => x.End);

            return new Interval<T>(
                min.Start,
                max.End,
                min.StartInclusive,
                max.EndInclusive);
        }

        [Pure]
        public static IEnumerable<Interval<T>> UnionAll<T>(this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Reduce((a, b) => a.Union(b));

        [Pure]
        public static IEnumerable<Interval<T>> ExceptAll<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Except(b), x => !x.IsEmpty());

        [Pure]
        public static IEnumerable<Interval<T>> IntersectAll<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Intersect(b), x => !x.IsEmpty());

        [Pure]
        public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => values.Pairwise((a, b) => a.Gap(b), x => !x.IsEmpty());

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

        [Pure]
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, TResult> resultSelector)
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
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, TResult?> resultSelector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct
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
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, TResult> resultSelector,
            Func<TResult, bool> predicate)
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
        public static IEnumerable<TResult> Pairwise<T, TResult>(
            this IEnumerable<Interval<T>> values,
            Func<Interval<T>, Interval<T>, TResult?> resultSelector,
            Func<TResult, bool> predicate)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct
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
