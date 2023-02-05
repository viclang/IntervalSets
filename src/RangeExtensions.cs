using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeExtensions.Configurations;

namespace RangeExtensions
{
    internal static class RangeExtensions
    {
        internal static void ExpandTo<TSource, TValue>(this TSource range, TSource? nextRange, Func<TValue, TValue> transform)
            where TSource : IRange<TValue>
            where TValue : struct, IComparable<TValue>, IComparable
        {
            if(nextRange is not null && range.To is null)
            {
                range.To = transform(nextRange.From);
            }
        }

        public static bool OverlapsWith<TSource>(this TSource source, TSource value)
        where TSource : struct, IRange<TSource>, IComparable<TSource>, IComparable
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
