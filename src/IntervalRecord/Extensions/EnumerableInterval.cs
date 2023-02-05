namespace IntervalRecord
{
    public static partial class Interval
    {
        public static IEnumerable<Interval<T>> Complement<T>(
            this IEnumerable<Interval<T>> values)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            using var e = values.GetEnumerator();

            if (!e.MoveNext())
                yield break;

            var previous = e.Current;
            while (e.MoveNext())
            {
                if (previous.IsBefore(e.Current))
                {
                    var gap = new Interval<T>(previous.End, e.Current.Start, !previous.EndInclusive, !e.Current.StartInclusive);
                    if (!gap.IsEmpty())
                    {
                        yield return gap;
                    }
                }
                else if (previous.IsAfter(e.Current))
                {
                    var gap = new Interval<T>(e.Current.End, previous.Start, !e.Current.EndInclusive, !previous.StartInclusive);
                    if (!gap.IsEmpty())
                    {
                        yield return gap;
                    }
                }
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
