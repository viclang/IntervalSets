using RangeExtensions.Configurations;

namespace RangeExtensions.Interfaces
{
    public static class RangeInclusiveExtensions
    {
        private static Configuration _configuration = new Configuration();

        public static bool HasValidRange<TSource>(this TSource value)
            where TSource : IRangeInclusive<int>
        {
            return value.To is null
                || value.To is not null && value.To!.Value <= value.From;
        }

        public static void Add<T>(
            this IList<T> ranges,
            T value)
            where T : IRangeInclusive<int>
        {
            if (!value.HasValidRange())
            {
                throw new NotSupportedException("To cannot be smaller than from!");
            }

            if (ranges.Any())
            {
                var from = ranges.Min(x => x.From);
                var last = ranges.GetLastRange();
                var replaceTo = value.From-1;

                if (OverlapsWith(from, last!.To ?? replaceTo, value.From, value.To))
                {
                    throw new NotSupportedException("Collection range has overlap with value to add!");
                }

                if (last.To is null)
                {
                    last.To = replaceTo;
                }
            }
            ranges.Add(value);
        }

        private static void AddRanged<TSource>(
            this IList<TSource> ranges,
            TSource value,
            Func<int, int> previousTo,
            Action<Configuration> options)
            where TSource : IRangeInclusive<int>
        {
            var strategy = new Configuration();
            options(strategy);

            if (!value.HasValidRange())
            {
                throw new NotSupportedException("To cannot be smaller than from!");
            }

            if (ranges.Any())
            {
                if (ranges.Any(x => x.From.Equals(value.From)))
                {
                    throw new NotSupportedException("From of new item does already exist!");
                }

                ranges = ranges.OrderBy(x => x.From).ToList();
                
                var previous = value.GetPreviousRange(ranges);
                if (previous is not null)
                {
                    previous.To = previousTo(value.From);
                }

                var next = value.GetNextRange(ranges);
                if (next is not null)
                {
                    if (value.To is null
                        || value.To is not null
                            && value.To.Value.CompareTo(next.From) > 0)
                    {
                        throw new NotSupportedException("From of new item does already exist!");
                    }
                }
            }

            ranges.Add(value);
        }

        private static T? GetPreviousRange<T>(this T value, IList<T> ranges)
            where T : IRangeInclusive<int>
        {
            return ranges.LastOrDefault(x => x.From.CompareTo(value.From) < 0);
        }

        private static T? GetNextRange<T>(this T value, IList<T> ranges)
            where T : IRangeInclusive<int>
        {
            return ranges.FirstOrDefault(x => x.From.CompareTo(value.From) > 0);
        }


        private static void AddNotOverlapping<T>(
            this IList<T> ranges,
            T value,
            Func<T, T> previousTo)
            where T : struct, IRange<T>, IComparable<T>, IComparable
        {
            if (ranges.Any(x => x.OverlapsWith(value)))
            {
                throw new NotSupportedException("Found one or more overlapping ranges!");
            }
            ranges.Add(value);
        }        

        private static TSource? GetFirstRange<TSource>(this IList<TSource> ranges)
            where TSource : IRangeInclusive<int>
        {
            return ranges.MinBy(x => x.From);
        }

        private static TSource? GetLastRange<TSource>(this IList<TSource> ranges)
            where TSource : IRangeInclusive<int>
        {
            return ranges.MaxBy(x => x.From);
        }

        private static (TProperty from, TProperty? to) GetCollectionRange<TSource, TProperty>(this IList<TSource> ranges)
            where TSource : IRangeInclusive<TProperty>
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (!ranges.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }

            return (ranges.Min(x => x.From),
                ranges.Any(x => x.To is null) ? null
                : ranges.Max(x => x.To!.Value));
        }

        public static bool OverlapsWith<TSource>(this TSource source, TSource value)
        where TSource : IRangeInclusive<int>
        {
            return OverlapsWith(source.From, source.To, value.From, value.To);
        }

        private static bool OverlapsWith<TProperty>(
            TProperty sourceFrom,
            TProperty? sourceTo,
            TProperty valueFrom,
            TProperty? valueTo)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {

            if (sourceTo is null
                && valueTo is null)
            {
                return true;
            }

            if (sourceTo is null
                && valueTo is not null)
            {
                return sourceFrom.CompareTo(valueFrom) <= 0
                    || sourceFrom.CompareTo(valueTo) <= 0;
            }

            if (sourceTo is not null
                && valueTo is null)
            {
                return valueFrom.CompareTo(sourceFrom) <= 0
                    || valueFrom.CompareTo(sourceTo) <= 0;
            }

            return sourceFrom.CompareTo(valueTo) <= 0
                && valueFrom.CompareTo(sourceTo) <= 0;
        }
    }
}