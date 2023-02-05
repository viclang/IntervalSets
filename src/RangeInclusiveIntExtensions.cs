using RangeExtensions.Configurations;

namespace RangeExtensions.Interfaces
{
    public static class RangeInclusiveIntExtensions
    {
        public static bool HasValidRange<TSource>(this TSource value)
            where TSource : IRangeInclusive<int>
        {
            return value.To is null
                || value.To is not null && value.To!.Value >= value.From;
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

                //if (OverlapsWith(from, last!.To ?? replaceTo, value.From, value.To))
                //{
                //    throw new NotSupportedException("Collection range has overlap with value to add!");
                //}

                if (last!.To is null)
                {
                    last.To = replaceTo;
                }
            }
            ranges.Add(value);
        }

        public static void RemoveAllBefore<TSource>(this List<TSource> ranges, int value)
            where TSource : IRangeInclusive<int>
        {
            ranges.RemoveAll(x => x.To < value);
        }

        public static void RemoveAllAfter<TSource>(this List<TSource> ranges, int value)
            where TSource : IRangeInclusive<int>
        {
            ranges.RemoveAll(x => x.From > value);
        }

        public static void InsertShrinkPrevious<TSource>(this IList<TSource> ranges, TSource value)
            where TSource : IRangeInclusive<int>
        {
            var previous = value.GetPreviousRange(ranges);
            if (previous is not null)
            {
                previous.To = value.From - 1;
            }
            
            var next = value.GetNextRange(ranges);
            if (next is not null)
            {
                if(value.To is null)
                {
                    value.To = next.From - 1;
                }

                if (ranges.Any(x => x.OverlapsWith(value)))
                {
                    throw new NotSupportedException("Found overlap");
                }                
            }            
            ranges.Add(value);
        }

        private static void AddRanged<TSource>(
            this IList<TSource> ranges,
            TSource value,
            Func<int, int> previousTo,
            Action<AddStrategy> options)
            where TSource : IRangeInclusive<int>
        {
            var strategy = new AddStrategy();
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

        private static T? GetPreviousRange<T>(this T value, IEnumerable<T> ranges)
            where T : IRangeInclusive<int>
        {
            return ranges
                .Where(x => x.From < value.From )
                .MaxBy(x => x.From);
        }

        private static T? GetNextRange<T>(this T value, IEnumerable<T> ranges)
            where T : IRangeInclusive<int>
        {
            return ranges
                .Where(x => x.From > value.From)
                .MinBy(x => x.From);
        }    

        

        private static (TProperty from, TProperty? to) GetCollectionRange<TSource, TProperty>(this IEnumerable<TSource> ranges)
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

        public static bool IsConnected<TSource>(this TSource source, TSource value)
        where TSource : IRangeInclusive<int>
        {
            return IsConnected(source.From, source.To, value.From, value.To);
        }

        private static bool IsConnected(
            int sourceFrom,
            int? sourceTo,
            int valueFrom,
            int? valueTo)
        {
            return sourceTo.IsConnected(valueFrom) || valueTo.IsConnected(sourceFrom);
        }
        private static bool IsConnected(this int? to, int from)
        {
            var exclusive = GetRangeExclusive(from, to);
            return exclusive.to.Equals(exclusive.from);
        }

        public static IRangeExclusive<int> ToExclusive(this IRangeInclusive<int> range)
        {
            range.To = range.To.GetExlcusiveTo();
            return (IRangeExclusive<int>)range;
        }

        public static IEnumerable<IRangeExclusive<int>> ToExclusive<TSource>(this IEnumerable<TSource> ranges)
            where TSource : IRangeInclusive<int>
        {
            foreach(var range in ranges)
            {
                yield return range.ToExclusive();
            }
        }

        private static (int from, int? to) GetRangeExclusive(int from, int? to)
        {
            return to is null
                ? (from, to)
                : (from, to.GetExlcusiveTo());
        }

        private static int? GetExlcusiveTo(this int? to)
        {
            return to is null ? to : to + 1;
        }

        private static bool OverlapsWith(
            int sourceFrom,
            int? sourceTo,
            int valueFrom,
            int? valueTo)
        {

            if (sourceTo is null
                && valueTo is null)
            {
                return true;
            }

            var after = sourceFrom > valueTo;
            if (sourceTo is null
                && valueTo is not null)
            {
                return !after;
            }

            var before = sourceTo < valueFrom;
            if (sourceTo is not null
                && valueTo is null)
            {
                return !before;
            }

            return !before && !after;
        }
    }
}