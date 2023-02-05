﻿using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<int> Closure(this Interval<int> source, int step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        [Pure]
        public static Interval<double> Closure(this Interval<double> source, double step)
            => ToClosed(source, start => start.Add(step), end => end.Substract(step));

        [Pure]
        public static Interval<DateTime> Closure(this Interval<DateTime> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        [Pure]
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        [Pure]
        public static Interval<DateOnly> Closure(this Interval<DateOnly> source, int step)
            => ToClosed(source, start => start.AddDays(step), end => end.AddDays(-step));

        [Pure]
        public static Interval<TimeOnly> Closure(this Interval<TimeOnly> source, TimeSpan step)
            => ToClosed(source, start => start.Add(step), end => end.Add(-step));

        private static Interval<T> ToClosed<T>(
            Interval<T> source,
            Func<Infinity<T>, Infinity<T>> add,
            Func<Infinity<T>, Infinity<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || source.StartInclusive && source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive ? source.Start : add(source.Start),
                End = source.EndInclusive ? source.End : substract(source.End),
                StartInclusive = true,
                EndInclusive = true
            };
        }
    }
}
