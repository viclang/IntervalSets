namespace RangeExtensions
{
    public static class RangeExtensions
    {
        //hard To: throw exception when overlapping 
        //soft To: change when overlapping

        public static void AddInclusive<TSource>(
            this IList<TSource> ranges,
            TSource value)
            where TSource : IRange<int>
        {
            ranges.AddRanged<TSource, int>(value, x => x - 1);
        }

        private static void AddRanged<TSource, TValue>(
            this IList<TSource> ranges,
            TSource value,
            Func<TValue, TValue> previousTo)
            where TSource : IRange<TValue>
            where TValue : struct, IComparable<TValue>, IComparable
        {
            if (ranges.Any())
            {
                if (ranges.Any(x => x.From.Equals(value.From)))
                {
                    throw new NotSupportedException("From of new item does already exist!");
                }

                TSource? previous = ranges.LastOrDefault(x => x.From.CompareTo(value.From) < 0);
                if (previous is not null)
                {
                    previous.To = previousTo(value.From);
                }

                TSource? next = ranges.FirstOrDefault(x => x.From.CompareTo(value.From) > 0);
                if (next is not null)
                {
                    if (value.To is null
                        || value.To is not null
                            && value.To.Value.CompareTo(next.From) > 0)
                    {
                        value.To = previousTo(next.From);
                    }
                }
            }
            ranges.Add(value);
        }

        private static void AddNotOverlapping<T>(
            this IList<T> ranges,
            T value,
            Func<T, T> previousTo)
            where T : struct, IRange<T>, IComparable<T>, IComparable
        {
            if (ranges.Any(x => x.OverlapsWith(value, previousTo)))
            {
                throw new NotSupportedException("Found one or more overlapping ranges!");
            }
            ranges.Add(value);
        }

        private static bool OverlapsWith<T>(this T source, T value, Func<T, T> previousTo)
        where T : struct, IRange<T>, IComparable<T>, IComparable
        {
            if (source.To is null)
            {
                return source.From.CompareTo(value.From) <= 0;
            }

            if (value.To is null)
            {
                return value.From.CompareTo(source.From) <= 0;
            }

            return source.From.CompareTo(value.To) <= 0
                && value.From.CompareTo(source.To) <= 0;
        }
    }
}