﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static double? Radius(this Interval<int> source)
        => Radius(source, (end, start) => ((double)end - start) / 2);

        [Pure]
        public static double? Radius(this Interval<double> source)
            => Radius(source, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTime> source)
            => Radius(source, (end, start) => (end - start) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<DateTimeOffset> source)
            => Radius(source, (end, start) => (end - start) / 2);

        [Pure]
        public static int? Radius(this Interval<DateOnly> source)
            => Radius(source, (end, start) => (end.DayNumber - start.DayNumber) / 2);

        [Pure]
        public static TimeSpan? Radius(this Interval<TimeOnly> source)
            => Radius(source, (end, start) => (end - start) / 2);

        private static TResult? Radius<T, TResult>(Interval<T> source, Func<T, T, TResult> radius)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            if (source.GetBoundedState() != BoundedState.Bounded || source.IsEmpty())
            {
                return null;
            }
            return radius(source.End.GetValueOrDefault(), source.Start.GetValueOrDefault());
        }
    }
}