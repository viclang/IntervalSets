namespace IntervalRecord
{
    public static partial class Interval
    {
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

        public static IEnumerable<Interval<T>> PairwiseNotEmpty<T>(
            this IEnumerable<Interval<T>> values, Func<Interval<T>, Interval<T>, Interval<T>> resultSelector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);

                if (!result.IsEmpty())
                    yield return result;
                
                previous = e.Current;
            }
        }

        public static IEnumerable<Interval<T>> PairwiseNotNullOrEmpty<T>(
            this IEnumerable<Interval<T>> values, Func<Interval<T>, Interval<T>, Interval<T>?> resultSelector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                var result = resultSelector(previous, e.Current);

                if (result != null && !result.Value.IsEmpty())
                    yield return result.Value;

                previous = e.Current;
            }
        }

        public static Interval<T> Hull<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!values.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }
            var min = values.MinBy(x => x.Start);
            var max = values.MaxBy(x => x.End);

            return new Interval<T>(
                min.Start,
                max.End,
                min.StartInclusive,
                max.EndInclusive);
        }
    }
}
