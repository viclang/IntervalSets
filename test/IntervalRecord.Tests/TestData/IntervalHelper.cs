using InfinityComparable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord.Tests.TestData
{
    public static class IntervalHelper
    {

        public static (Interval<T> Closed, Interval<T> ClosedOpen, Interval<T> OpenClosed, Interval<T> Open) CreateAllTypes<T>(Infinity<T> start, Infinity<T> end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return (
                new Interval<T>(start, end, true, true),
                new Interval<T>(start, end, true, false),
                new Interval<T>(start, end, false, true),
                new Interval<T>(start, end, false, false));
        }

        public static (TResult Closed, TResult ClosedOpen, TResult OpenClosed, TResult Open) Execute<T, TResult>(this (Interval<T> Closed, Interval<T> ClosedOpen, Interval<T> OpenClosed, Interval<T> Open) value, Func<Interval<T>, TResult> func)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
        {
            return (func(value.Closed), func(value.ClosedOpen), func(value.OpenClosed), func(value.Open));
        }
    }
}
