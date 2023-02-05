namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Canonicalize<T>(this Interval<T> value, BoundaryType intervalType, int step, Func<T, int, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ConvertTo(value, intervalType, x => addStep(x, step), x => addStep(x, -step));
        public static Interval<T> Canonicalize<T>(this Interval<T> value, BoundaryType intervalType, double step, Func<T, double, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ConvertTo(value, intervalType, x => addStep(x, step), x => addStep(x, -step));
        public static Interval<T> Canonicalize<T>(this Interval<T> value, BoundaryType intervalType, TimeSpan step, Func<T, TimeSpan, T> addStep)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => ConvertTo(value, intervalType, x => addStep(x, step), x => addStep(x, -step));

        private static Interval<T> ConvertTo<T>(
            this Interval<T> value,
            BoundaryType intervalType,
            Func<T, T> add,
            Func<T, T> substract)
                where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => intervalType switch
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
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
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
