using IntervalRecords.Extensions;
using Unbounded;

namespace IntervalRecords.Tests;
public static class UnboundedHelper
{
    public static Unbounded<T> Substract<T, TOffset>(this Unbounded<T> left, Unbounded<TOffset> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, ISpanParsable<TOffset>
    {
        return (left, right) switch
        {
            (Unbounded<int> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedMathHelper.Substract(a, b),
            (Unbounded<double> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedMathHelper.Substract(a, b),
            (Unbounded<DateTime> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedDateTime.Substract(a, b),
            (Unbounded<DateTimeOffset> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedDateTimeOffset.Substract(a, b),
            (Unbounded<DateOnly> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedDateOnly.AddDays(a, -b.GetValueOrDefault()),
            (Unbounded<TimeOnly> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedTimeOnly.AddHours(a, -b.GetValueOrDefault()),
            _ => throw new NotImplementedException(),
        };
    }

    public static Unbounded<T> Add<T, TOffset>(this Unbounded<T> left, Unbounded<TOffset> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, ISpanParsable<TOffset>
    {
        return (left, right) switch
        {
            (Unbounded<int> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedMathHelper.Add(a, b),
            (Unbounded<double> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedMathHelper.Add(a, b),
            (Unbounded<DateTime> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedDateTime.Add(a, b),
            (Unbounded<DateTimeOffset> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedDateTimeOffset.Add(a, b),
            (Unbounded<DateOnly> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedDateOnly.AddDays(a, b.GetValueOrDefault()),
            (Unbounded<TimeOnly> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedTimeOnly.AddHours(a, b.GetValueOrDefault()),
            _ => throw new NotImplementedException(),
        };
    }


    public static Unbounded<TResult> TLength<T, TResult>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, ISpanParsable<TResult>
    {
        return value switch
        {
            Interval<int> interval => (Unbounded<TResult>)(object)interval.Length(),
            Interval<double> interval => (Unbounded<TResult>)(object)interval.Length(),
            Interval<DateTime> interval => (Unbounded<TResult>)(object)interval.Length(),
            Interval<DateTimeOffset> interval => (Unbounded<TResult>)(object)interval.Length(),
            Interval<DateOnly> interval => (Unbounded<TResult>)(object)interval.Length(),
            Interval<TimeOnly> interval => (Unbounded<TResult>)(object)interval.Length(),
            _ => throw new NotImplementedException(),
        };
    }
}
