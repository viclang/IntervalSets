namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<int> Canonicalize(this Interval<int> value, BoundaryType boundaryType, int step) => Canonicalize(value, boundaryType, x => x + step, x => x - step);
        public static Interval<double> Canonicalize(this Interval<double> value, BoundaryType boundaryType, double step) => Canonicalize(value, boundaryType, x => x + step, x => x - step);
        public static Interval<DateOnly> Canonicalize(this Interval<DateOnly> value, BoundaryType boundaryType, int step) => Canonicalize(value, boundaryType, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<TimeOnly> Canonicalize(this Interval<TimeOnly> value, BoundaryType boundaryType, TimeSpan step) => Canonicalize(value, boundaryType, x => x.Add(step), x => x.Add(-step));
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> value, BoundaryType boundaryType, TimeSpan step) => Canonicalize(value, boundaryType, x => x + step, x => x - step);
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> value, BoundaryType boundaryType, TimeSpan step) => Canonicalize(value, boundaryType, x => x + step, x => x - step);

        private static Interval<T> Canonicalize<T>(
            this Interval<T> value,
            BoundaryType boundaryType,
            Func<T, T> add,
            Func<T, T> substract)
                where T : struct, IComparable<T>, IComparable
            => boundaryType switch
            {
                BoundaryType.Closed => ToClosed(value, add, substract),
                BoundaryType.ClosedOpen => ToClosedOpen(value, add),
                BoundaryType.OpenClosed => ToOpenClosed(value, substract),
                BoundaryType.Open => ToOpen(value, add, substract),
                _ => throw new NotImplementedException()
            };

        private static Interval<T> ToClosed<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || value.StartInclusive && value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Value),
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Value),
                StartInclusive = true,
                EndInclusive = true
            };
        }

        private static Interval<T> ToClosedOpen<T>(
            Interval<T> value,
            Func<T, T> add)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || value.StartInclusive && !value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Value),
                End = value.EndInclusive ? add(value.End.Value) : value.End,
                StartInclusive = true,
                EndInclusive = false
            };
        }

        private static Interval<T> ToOpenClosed<T>(
            Interval<T> value,
            Func<T, T> substract)
            where T : struct, IComparable<T>, IComparable
        {
            if (value.IsEmpty() || !value.StartInclusive && value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive ? substract(value.Start.Value) : value.Start,
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Value),
                StartInclusive = false,
                EndInclusive = true
            };
        }

        private static Interval<T> ToOpen<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IComparable<T>, IComparable
        {
            if (!value.StartInclusive && !value.EndInclusive)
            {
                return value;
            }
            return value with
            {
                Start = value.StartInclusive ? substract(value.Start.Value) : value.Start,
                End = value.EndInclusive ? add(value.End.Value) : value.End,
                StartInclusive = false,
                EndInclusive = false
            };
        }
    }
}
