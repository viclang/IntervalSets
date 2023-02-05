namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<int> Canonicalize(this Interval<int> value, BoundaryType intervalType, int step)
            => ConvertTo(value, intervalType, x => x + step, x => x - step);
        public static Interval<DateOnly> CanonicalizeDays(this Interval<DateOnly> value, BoundaryType intervalType, int step)
            => ConvertTo(value, intervalType, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<DateOnly> CanonicalizeMonths(this Interval<DateOnly> value, BoundaryType intervalType, int step)
            => ConvertTo(value, intervalType, x => x.AddMonths(step), x => x.AddMonths(-step));
        public static Interval<DateOnly> CanonicalizeYears(this Interval<DateOnly> value, BoundaryType intervalType, int step)
            => ConvertTo(value, intervalType, x => x.AddYears(step), x => x.AddYears(-step));
        public static Interval<DateTime> Canonicalize(this Interval<DateTime> value, BoundaryType intervalType, TimeSpan step)
            => ConvertTo(value, intervalType, x => x.Add(step), x => x.Subtract(step));
        public static Interval<DateTimeOffset> Canonicalize(this Interval<DateTimeOffset> value, BoundaryType intervalType, TimeSpan step)
            => ConvertTo(value, intervalType, x => x.Add(step), x => x.Subtract(step));

        public static Interval<int> Closure(this Interval<int> value, int step)
            => ToClosed(value, x => x + step, x => x - step);
        public static Interval<DateOnly> ClosureDays(this Interval<DateOnly> value, int step)
            => ToClosed(value, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<DateOnly> ClosureMonths(this Interval<DateOnly> value, int step)
            => ToClosed(value, x => x.AddMonths(step), x => x.AddMonths(-step));
        public static Interval<DateOnly> ClosureYears(this Interval<DateOnly> value, int step)
            => ToClosed(value, x => x.AddYears(step), x => x.AddYears(-step));
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Subtract(step));
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan step)
            => ToClosed(value, x => x.Add(step), x => x.Subtract(step));

        public static Interval<int> Interior(this Interval<int> value, int step)
            => ToOpen(value, x => x + step, x => x - step);
        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, Func<DateOnly, int, DateOnly> add, int step)
            => ToOpen(value, x => add(x, step), x => add(x, -step));
        public static Interval<DateTime> Interior(this Interval<DateTime> value, Func<DateTime, double, DateTime> add, double step)
            => ToOpen(value, x => add(x, step), x => add(x, -step));
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, Func<DateTimeOffset, double, DateTimeOffset> add, double step)
            => ToOpen(value, x => add(x, step), x => add(x, -step));
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Subtract(step));
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan step)
            => ToOpen(value, x => x.Add(step), x => x.Subtract(step));

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

        /// <summary>
        /// Closure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="add"></param>
        /// <param name="substract"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Interior
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="add"></param>
        /// <param name="substract"></param>
        /// <returns></returns>
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
