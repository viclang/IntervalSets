using IntervalRecords.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords.Tests;
public static class UnboundedHelper
{
    public static Unbounded<T> Substract<T, TOffset>(this Unbounded<T> left, Unbounded<TOffset> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, IComparable
    {
        return (left, right) switch
        {
            (Unbounded<int> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedExtensions.Substract(a, b),
            (Unbounded<double> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedExtensions.Substract(a, b),
            (Unbounded<DateTime> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedExtensions.Substract(a, b),
            (Unbounded<DateTimeOffset> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedExtensions.Substract(a, b),
            (Unbounded<DateOnly> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedExtensions.AddDays(a, -b.GetFiniteOrDefault()),
            (Unbounded<TimeOnly> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedExtensions.AddHours(a, -b.GetFiniteOrDefault()),
            _ => throw new NotImplementedException(),
        };
    }

    public static Unbounded<T> Add<T, TOffset>(this Unbounded<T> left, Unbounded<TOffset> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TOffset : struct, IEquatable<TOffset>, IComparable<TOffset>, IComparable
    {
        return (left, right) switch
        {
            (Unbounded<int> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedExtensions.Add(a, b),
            (Unbounded<double> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedExtensions.Add(a, b),
            (Unbounded<DateTime> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedExtensions.Add(a, b),
            (Unbounded<DateTimeOffset> a, Unbounded<TimeSpan> b) => (Unbounded<T>)(object)UnboundedExtensions.Add(a, b),
            (Unbounded<DateOnly> a, Unbounded<int> b) => (Unbounded<T>)(object)UnboundedExtensions.AddDays(a, b.GetFiniteOrDefault()),
            (Unbounded<TimeOnly> a, Unbounded<double> b) => (Unbounded<T>)(object)UnboundedExtensions.AddHours(a, b.GetFiniteOrDefault()),
            _ => throw new NotImplementedException(),
        };
    }


    public static Unbounded<TResult> TLength<T, TResult>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
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
