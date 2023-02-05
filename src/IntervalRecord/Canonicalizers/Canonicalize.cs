using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        [Pure]
        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> source, BoundaryType boundaryType, int step)
            => Canonicalize(source, boundaryType, b => b.AddDays(step), b => b.AddDays(-step));

        [Pure]
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> source, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));

        [Pure]
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> source, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));

        [Pure]
        public static Interval<double> Canonicalize(this Interval<double> source, BoundaryType boundaryType, double step)
            => Canonicalize(source, boundaryType, b => b + step, b => b - step);

        [Pure]
        public static Interval<int> Canonicalize(this Interval<int> source, BoundaryType boundaryType, int step)
            => Canonicalize(source, boundaryType, b => b + step, b => b - step);

        [Pure]
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> source, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(source, boundaryType, b => b.Add(step), b => b.Add(-step));


        [Pure]
        private static Interval<T> Canonicalize<T>(
            Interval<T> source,
            BoundaryType boundaryType,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => boundaryType switch
            {
                BoundaryType.Closed => ToClosed(source, add, substract),
                BoundaryType.ClosedOpen => ToClosedOpen(source, add),
                BoundaryType.OpenClosed => ToOpenClosed(source, substract),
                BoundaryType.Open => ToOpen(source, add, substract),
                _ => throw new NotImplementedException()
            };

        [Pure]
        private static Interval<T> ToClosed<T>(
            Interval<T> source,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || source.StartInclusive && source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive || source.Start.IsInfinite ? source.Start : add(source.Start.Finite.Value),
                End = source.EndInclusive || source.End.IsInfinite ? source.End : substract(source.End.Finite.Value),
                StartInclusive = true,
                EndInclusive = true
            };
        }

        [Pure]
        private static Interval<T> ToClosedOpen<T>(
            Interval<T> source,
            Func<T, T> add)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || source.StartInclusive && !source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive || source.Start.IsInfinite ? source.Start : add(source.Start.Finite.Value),
                End = source.EndInclusive && !source.End.IsInfinite ? add(source.End.Finite.Value) : source.End,
                StartInclusive = true,
                EndInclusive = false
            };
        }

        [Pure]
        private static Interval<T> ToOpenClosed<T>(Interval<T> source, Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (source.IsEmpty() || !source.StartInclusive && source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive && !source.Start.IsInfinite ? substract(source.Start.Finite.Value) : source.Start,
                End = source.EndInclusive || source.End.IsInfinite ? source.End : substract(source.End.Finite.Value),
                StartInclusive = false,
                EndInclusive = true
            };
        }

        [Pure]
        private static Interval<T> ToOpen<T>(Interval<T> source, Func<T, T> add, Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!source.StartInclusive && !source.EndInclusive)
            {
                return source;
            }
            return source with
            {
                Start = source.StartInclusive && !source.Start.IsInfinite ? substract(source.Start.Finite.Value) : source.Start,
                End = source.EndInclusive && !source.End.IsInfinite ? add(source.End.Finite.Value) : source.End,
                StartInclusive = false,
                EndInclusive = false
            };
        }
    }
}
