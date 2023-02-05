using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions
{
    public static class RangeExtensions
    {
        public static IEnumerable<IRangeInclusive<int>> AsInclusive(this IEnumerable<IRange<int>> ranges)
        {
            return (IEnumerable<IRangeInclusive<int>>)ranges;
        }

        public static IEnumerable<IRangeExclusive<int>> AsExclusive(this IEnumerable<IRange<int>> ranges)
        {
            return (IEnumerable<IRangeExclusive<int>>)ranges;
            //return ranges.Select(r => r.AsExclusive());
        }

        public static IRangeInclusive<int> AsInclusive(this IRange<int> range)
        {
            return (IRangeInclusive<int>)range;
        }

        public static IRangeExclusive<int> AsExclusive(this IRange<int> range)
        {
            return (IRangeExclusive<int>)range;
        }
    }
}
