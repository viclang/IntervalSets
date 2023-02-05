using InfinityComparable;

namespace IntervalRecord
{
    public interface IIntervalConverter<T, TStep>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TStep : struct
    {
        Interval<T> Canonicalize(BoundaryType boundaryType, TStep step);
        Interval<T> Closure(TStep step);
        Interval<T> Interior(TStep step);
    }

    public interface IIntervalMeasurements<TLengthResult, TRadiusResult, TCentreResult>
        where TLengthResult : struct, IEquatable<TLengthResult>, IComparable<TLengthResult>, IComparable
        where TRadiusResult : struct
        where TCentreResult : struct
    {
        Infinity<TLengthResult> Length();
        TRadiusResult? Radius();
        TCentreResult? Centre();
    }

    public abstract class AbstractInterval<T>
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        protected static Interval<T> Canonicalize(
            Interval<T> value,
            BoundaryType boundaryType,
            Func<T, T> add,
            Func<T, T> substract)
            => boundaryType switch
            {
                BoundaryType.Closed => ToClosed(value, add, substract),
                BoundaryType.ClosedOpen => ToClosedOpen(value, add),
                BoundaryType.OpenClosed => ToOpenClosed(value, substract),
                BoundaryType.Open => ToOpen(value, add, substract),
                _ => throw new NotImplementedException()
            };

        protected static Interval<T> ToClosed(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
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

        protected static Interval<T> ToClosedOpen(
            Interval<T> value,
            Func<T, T> add)
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

        protected static Interval<T> ToOpenClosed(
            Interval<T> value,
            Func<T, T> substract)
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

        protected static Interval<T> ToOpen(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
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

        protected static Infinity<TResult> Length<TResult>(Interval<T> value, Func<T, T, TResult> substract)
            where TResult : struct, IEquatable<TResult>, IComparable<TResult>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? Infinity<TResult>.PositiveInfinity
                : value.IsEmpty() ? default : substract(value.End.Finite.Value, value.Start.Finite.Value);

        protected static TResult? CalculateOrNull<TResult>(Interval<T> value, Func<T, T, TResult> centre)
            where TResult : struct
            => value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : centre(value.End.Finite.Value, value.Start.Finite.Value);
    }
}
