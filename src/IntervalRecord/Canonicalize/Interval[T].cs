using InfinityComparable;
using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        private static Interval<T> Canonicalize<T>(
            Interval<T> source,
            IntervalType boundaryType,
            Func<Infinity<T>, Infinity<T>> add,
            Func<Infinity<T>, Infinity<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => boundaryType switch
            {
                IntervalType.Closed => ToClosed(source, add, substract),
                IntervalType.ClosedOpen => ToClosedOpen(source, add),
                IntervalType.OpenClosed => ToOpenClosed(source, substract),
                IntervalType.Open => ToOpen(source, add, substract),
                _ => throw new NotImplementedException()
            };

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

        private static Interval<T> ToOpen<T>(Interval<T> source, Func<Infinity<T>, Infinity<T>> add, Func<Infinity<T>, Infinity<T>> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!source.StartInclusive && !source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive ? substract(source.Start) : source.Start,
                End = source.EndInclusive ? add(source.End) : source.End,
                StartInclusive = false,
                EndInclusive = false
            };
        }
    }
}
