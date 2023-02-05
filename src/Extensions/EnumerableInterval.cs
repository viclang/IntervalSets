
namespace IntervalRecord.Extensions
{
    public static class EnumerableInterval
    {
        //public static bool IsConnected(
        //    this Interval<DateOnly> value,
        //    Interval<DateOnly> other,
        //    bool hasInclusiveEnd)
        //{
        //    return value.End is not null
        //           && other.Start.Equals(
        //               hasInclusiveEnd
        //                   ? value.End.Value.AddDays(1)
        //                   : value.End.Value);
        //}

        //public static bool IsConnected(
        //    this Interval<DateTime> value,
        //    Interval<DateTime> other,
        //    bool hasInclusiveEnd)
        //{
        //    return value.End is not null
        //           && other.Start.Equals(
        //               hasInclusiveEnd
        //                   ? value.End.Value.AddDays(1)
        //                   : value.End.Value);
        //}

        //public static bool IsConnected(
        //    this Interval<DateTimeOffset> value,
        //    Interval<DateTimeOffset> other,
        //    bool hasInclusiveEnd)
        //{
        //    return value.End is not null
        //           && other.Start.Equals(
        //               hasInclusiveEnd
        //                   ? value.End.Value.AddDays(1)
        //                   : value.End.Value);
        //}

        //public static bool IsConnected(
        //    this Interval<int> value,
        //    Interval<int> other,
        //    bool hasInclusiveEnd)
        //{
        //    return value.End is not null
        //           && other.Start.Equals(
        //               hasInclusiveEnd
        //                   ? value.End.Value + 1
        //                   : value.End.Value);
        //}

        
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
