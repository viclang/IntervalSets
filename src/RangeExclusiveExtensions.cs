using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions
{
    public static class RangeExclusiveExtensions
    {
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
            return to.Equals(from);
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

            var before = sourceFrom > valueTo;
            if (sourceTo is null
                && valueTo is not null)
            {
                return !before && !valueTo.IsConnected(sourceFrom);
            }

            var after = sourceTo < valueFrom;
            if (sourceTo is not null
                && valueTo is null)
            {
                return !after && !valueTo.IsConnected(sourceFrom);
            }

            return !before && !after
                && !IsConnected(sourceFrom, sourceTo, valueFrom, valueTo);
        }

        public static IRangeInclusive<int> ToInclusive(this IRangeExclusive<int> range)
        {
            range.To = range.To.GetInclusiveTo();
            return (IRangeInclusive<int>)range;
        }

        public static IEnumerable<IRangeInclusive<int>> ToInclusive<TSource>(this IEnumerable<TSource> ranges)
            where TSource : IRangeExclusive<int>
        {
            foreach (var range in ranges)
            {
                yield return range.ToInclusive();
            }
        }
        private static int? GetInclusiveTo(this int? to)
        {
            return to is null ? to : to - 1;
        }
    }
}
