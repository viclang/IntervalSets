using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RangeExtensions.Comparers;
using RangeExtensions.Interfaces;

namespace RangeExtensions
{
    public static class RangeExtensions
    {
        public static readonly RangeComparer<int> RangeIntegerComparer = new(to => to - 1);

        public static readonly RangeComparer<DateTime> RangeDateTimeComparer = new(to => to.AddDays(-1));

        public static readonly RangeComparer<DateTimeOffset> RangeDateTimeOffsetComparer = new(to => to.AddDays(-1));

        public static readonly RangeComparer<DateOnly> RangeDateOnlyComparer = new(to => to.AddDays(-1));

        public static bool IsValidRange<TProperty>(
            this IRange<TProperty> value, bool isInclusive)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            var comparer = new RangeComparer<TProperty>(isInclusive);
            return value.To is null 
                   || value.To is not null
                   && comparer.Compare(value.To!.Value, value.From) is 1;
        }

        private static (TProperty from, TProperty? to) GetCollectionRange<TSource, TProperty>(
            this IEnumerable<TSource> values)
            where TSource : IRangeInclusive<TProperty>
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            if (!values.Any())
            {
                throw new NotSupportedException("Collection is empty");
            }

            return (values.Min(x => x.From),
                values.Any(x => x.To is null) ? null
                    : values.Max(x => x.To!.Value));
        }

        public static bool IsConnected<TSource, TProperty>(
            this TSource source,
            TSource value,
            Func<TProperty, TProperty> transformTo)
            where TSource : IRange<TProperty>
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return source.To.IsConnected(value.From, transformTo);
        }

        private static bool IsConnected<TProperty>(
            this TProperty? to,
            TProperty from,
            Func<TProperty, TProperty> transformTo)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return to is not null && transformTo(to.Value).Equals(from);
        }
    }
}
