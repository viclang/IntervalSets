namespace IntervalRecord
{
    public sealed class IntIntervalConverter : AbstractIntervalConverter<int, int>
    {
        public override Interval<int> Canonicalize(Interval<int> value, BoundaryType boundaryType, int step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public override Interval<int> Closure(Interval<int> value, int step)
            => ToClosed(value, x => x + step, x => x - step);

        public override Interval<int> Interior(Interval<int> value, int step)
            => ToOpen(value, x => x + step, x => x - step);
    }

    public sealed class DoubleIntervalConverter : AbstractIntervalConverter<double, double>
    {
        public override Interval<double> Canonicalize(Interval<double> value, BoundaryType boundaryType, double step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public override Interval<double> Closure(Interval<double> value, double step)
            => ToClosed(value, x => x + step, x => x - step);

        public override Interval<double> Interior(Interval<double> value, double step)
            => ToOpen(value, x => x + step, x => x - step);
    }

    public sealed class DateOnlyIntervalConverter : AbstractIntervalConverter<DateOnly, int>
    {
        public override Interval<DateOnly> Canonicalize(Interval<DateOnly> value, BoundaryType boundaryType, int step)
            => Canonicalize(value, boundaryType, x => x.AddDays(step), x => x.AddDays(-step));

        public override Interval<DateOnly> Closure(Interval<DateOnly> value, int step)
            => ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));

        public override Interval<DateOnly> Interior(Interval<DateOnly> value, int step)
            => ToOpen(value, x => x.AddDays(step), x => x.AddDays(-step));
    }

    public sealed class TimeOnlyIntervalConverter : AbstractIntervalConverter<TimeOnly, TimeSpan>
    {
        public override Interval<TimeOnly> Canonicalize(Interval<TimeOnly> value, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));

        public override Interval<TimeOnly> Closure(Interval<TimeOnly> value, TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Add(-step));

        public override Interval<TimeOnly> Interior(Interval<TimeOnly> value, TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Add(-step));
    }

    public sealed class DateTimeIntervalConverter : AbstractIntervalConverter<DateTime, TimeSpan>
    {
        public override Interval<DateTime> Canonicalize(Interval<DateTime> value, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public override Interval<DateTime> Closure(Interval<DateTime> value, TimeSpan step)
            => ToClosed(value, x => x + step, x => x - step);

        public override Interval<DateTime> Interior(Interval<DateTime> value, TimeSpan step)
            => ToOpen(value, x => x + step, x => x - step);
    }

    public sealed class DateTimeOffsetIntervalConverter : AbstractIntervalConverter<DateTimeOffset, TimeSpan>
    {
        public override Interval<DateTimeOffset> Canonicalize(Interval<DateTimeOffset> value, BoundaryType boundaryType, TimeSpan step)
            => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        public override Interval<DateTimeOffset> Closure(Interval<DateTimeOffset> value, TimeSpan step)
            => ToClosed(value, x => x + step, x => x - step);

        public override Interval<DateTimeOffset> Interior(Interval<DateTimeOffset> value, TimeSpan step)
            => ToOpen(value, x => x + step, x => x - step);
    }

    public abstract class AbstractIntervalConverter<T, TStep>
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TStep : struct, IEquatable<TStep>, IComparable<TStep>, IComparable
    {
        public abstract Interval<T> Canonicalize(Interval<T> value, BoundaryType boundaryType, TStep step);
        public abstract Interval<T> Closure(Interval<T> value, TStep step);
        public abstract Interval<T> Interior(Interval<T> value, TStep step);

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
    }
}
