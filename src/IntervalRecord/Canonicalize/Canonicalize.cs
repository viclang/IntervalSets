using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<int> Canonicalize(this Interval<int> source, IntervalType intervalType, int step)
            => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<double> Canonicalize(this Interval<double> source, IntervalType intervalType, double step)
            => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, IntervalType intervalType, TimeSpan step)
            => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, IntervalType intervalType, TimeSpan step)
            => Canonicalize(source, intervalType, b => b.Add(step), b => b.Substract(step));

        [Pure]
        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, IntervalType intervalType, int step)
            => Canonicalize(source, intervalType, b => b.AddDays(step), b => b.AddDays(-step));

        [Pure]
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, IntervalType intervalType, TimeSpan step)
            => Canonicalize(source, intervalType, b => b.Add(step), b => b.Add(-step));

        private static Interval<T> Canonicalize<T>(
            Interval<T> source,
            IntervalType intervalType,
            Func<Infinity<T>, Infinity<T>> add,
            Func<Infinity<T>, Infinity<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => intervalType switch
            {
                IntervalType.Closed => ToClosed(source, add, substract),
                IntervalType.ClosedOpen => ToClosedOpen(source, add),
                IntervalType.OpenClosed => ToOpenClosed(source, substract),
                IntervalType.Open => ToOpen(source, add, substract),
                _ => throw new NotImplementedException()
            };

        private static Interval<T> ToClosedOpen<T>(
            Interval<T> source,
            Func<Infinity<T>, Infinity<T>> add)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || source.StartInclusive && !source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive ? source.Start : add(source.Start),
                End = source.EndInclusive ? add(source.End) : source.End,
                StartInclusive = true,
                EndInclusive = false
            };
        }

        private static Interval<T> ToOpenClosed<T>(Interval<T> source, Func<Infinity<T>, Infinity<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || !source.StartInclusive && source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive ? substract(source.Start) : source.Start,
                End = source.EndInclusive ? source.End : substract(source.End),
                StartInclusive = false,
                EndInclusive = true
            };
        }
    }
}
