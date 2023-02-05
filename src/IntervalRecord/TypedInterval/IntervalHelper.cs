using InfinityComparable;

namespace IntervalRecord
{
    internal static class IntervalHelper
    {
        internal static Interval<T> Canonicalize<T>(
            Interval<T> value,
            BoundaryType boundaryType,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => boundaryType switch
            {
                BoundaryType.Closed => ToClosed(value, add, substract),
                BoundaryType.ClosedOpen => ToClosedOpen(value, add),
                BoundaryType.OpenClosed => ToOpenClosed(value, substract),
                BoundaryType.Open => ToOpen(value, add, substract),
                _ => throw new NotImplementedException()
            };

        internal static Interval<T> ToClosed<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || value.StartInclusive && value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Finite.Value),
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Finite.Value),
                StartInclusive = true,
                EndInclusive = true
            };
        }

        internal static Interval<T> ToClosedOpen<T>(
            Interval<T> value,
            Func<T, T> add)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || value.StartInclusive && !value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Finite.Value),
                End = value.EndInclusive && !value.End.IsInfinite ? add(value.End.Finite.Value) : value.End,
                StartInclusive = true,
                EndInclusive = false
            };
        }

        internal static Interval<T> ToOpenClosed<T>(Interval<T> value, Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || !value.StartInclusive && value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive && !value.Start.IsInfinite ? substract(value.Start.Finite.Value) : value.Start,
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Finite.Value),
                StartInclusive = false,
                EndInclusive = true
            };
        }

        internal static Interval<T> ToOpen<T>(Interval<T> value, Func<T, T> add, Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.StartInclusive && !value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive && !value.Start.IsInfinite ? substract(value.Start.Finite.Value) : value.Start,
                End = value.EndInclusive && !value.End.IsInfinite ? add(value.End.Finite.Value) : value.End,
                StartInclusive = false,
                EndInclusive = false
            };
        }

        internal static Infinity<TResult> ValueOrInfinity<T, TResult>(Interval<T> value, Func<T, T, TResult> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? Infinity<TResult>.PositiveInfinity
                : value.IsEmpty() ? default : substract(value.End.Finite.Value, value.Start.Finite.Value);

        internal static TResult? ValueOrNull<T, TResult>(Interval<T> value, Func<T, T, TResult> centre)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : centre(value.End.Finite.Value, value.Start.Finite.Value);
    }
}
