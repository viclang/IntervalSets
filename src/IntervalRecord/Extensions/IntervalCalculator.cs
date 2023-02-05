using InfinityComparable;

namespace IntervalRecord
{
    public static partial class Interval
    {
        public static Interval<T> Intersect<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.IsConnected(other))
            {
                throw new ArgumentOutOfRangeException("other", "Intersection is only supported for Overlapping intervals.");
            }
            var maxByStart = MaxBy(value, other, x => x.Start);
            var minByEnd = MinBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive && other.StartInclusive
                : maxByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive && other.EndInclusive
                : minByEnd.EndInclusive;

            return value with { Start = maxByStart.Start, End = minByEnd.End, StartInclusive = startInclusive, EndInclusive = endInclusive };
        }

        public static Interval<T> Union<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.IsConnected(other))
            {
                throw new ArgumentOutOfRangeException("other", "Union is not continuous.");
            }
            return Hull(value, other);
        }

        public static Interval<T> Hull<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var minByStart = MinBy(value, other, x => x.Start);
            var maxByEnd = MaxBy(value, other, x => x.End);

            var startInclusive = value.Start == other.Start
                ? value.StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = value.End == other.End
                ? value.EndInclusive || other.EndInclusive
                : maxByEnd.EndInclusive;

            return new Interval<T>((T?)minByStart.Start, (T?)maxByEnd.End, startInclusive, endInclusive);
        }

        public static Interval<T> MinBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
        {
            if (selector(a).CompareTo(selector(b)) == -1)
            {
                return a;
            }
            return b;
        }

        public static Interval<T> MaxBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
        {
            if (selector(a).CompareTo(selector(b)) == 1)
            {
                return a;
            }
            return b;
        }

        //public static IEnumerable<DateOnly> EachDayOfInterval(this Interval<DateOnly> value, int step = 1)
        //{
        //    var closed = value.ClosureByDays(1);
        //    var length = closed.Length();
        //    if (!length.IsInfinite)
        //    {
        //        for(var days = 0; days <= length; days+=step)
        //        {
        //            yield return closed.Start.Value.AddDays(days);
        //        }
        //    }
        //}

        //public static IEnumerable<DateOnly> EachMonthOfInterval(this Interval<DateOnly> value, int step = 1)
        //{
        //    var closed = value.ClosureByDays(1);
        //    var length = closed.Length();
        //    if (!length.IsInfinite)
        //    {
        //        for (var months = 0; months <= length/12; months += step)
        //        {
        //            yield return closed.Start.Value.AddMonths(months);
        //        }
        //    }
        //}

        //public static IEnumerable<DateOnly> EachYearOfInterval(this Interval<DateOnly> value, int step = 1)
        //{
        //    var closed = value.ClosureByYears(1);
        //    var length = closed.Length();

        //    if (!length.IsInfinite)
        //    {
        //        for (var years = 0; years <= length/365; years += step)
        //        {
        //            yield return closed.Start.Value.AddYears(years);
        //        }
        //    }
        //}

        public static Infinity<int> Length(this Interval<int> value) => Calculate(Closure(value, 1), (a, b) => a - b);
        public static Infinity<int> Length(this Interval<DateOnly> value) => Calculate(ClosureDays(value, 1), (a, b) => a.DayNumber - b.DayNumber);
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value, TimeSpan closureStep) => CalculateEndStart(value.Closure(closureStep), (a, b) => a.Subtract(b));
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value, TimeSpan closureStep) => CalculateEndStart(value.Closure(closureStep), (a, b) => a.Subtract(b));

        public static Infinity<int> Radius(this Interval<int> value) => Calculate(value.Closure(1), (a, b) => (a - b) / 2);
        public static Infinity<int> Radius(this Interval<DateOnly> value) => Calculate(value.ClosureDays(1), (a, b) => (a.DayNumber - b.DayNumber) / 2);

        /// <summary>
        /// Radius
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval <see cref="Closure(Interval{DateTime}, TimeSpan)"/></param>
        /// <returns></returns>
        public static Infinity<TimeSpan> Radius(this Interval<DateTime> value, TimeSpan closureStep) => CalculateEndStart(value.Closure(closureStep), (a, b) => a.Subtract(b) / 2);
        /// <summary>
        /// Radius
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval before measurement <see cref="Closure(Interval{DateTimeOffset}, TimeSpan)"/></param>
        /// <returns></returns>
        public static Infinity<TimeSpan> Radius(this Interval<DateTimeOffset> value, TimeSpan closureStep) => CalculateEndStart(value.Closure(closureStep), (a, b) => a.Subtract(b) / 2);

        public static int? Centre(this Interval<int> value) => Centre(value.Closure(1), (a, b) => (a + b) / 2);
        public static DateOnly? Centre(this Interval<DateOnly> value) => Centre(value.ClosureDays(1), (a, b) => a.AddDays((int)Math.Round((double)(a.DayNumber + b.DayNumber) / 2)));
        public static DateTime? Centre(this Interval<DateTime> value, TimeSpan closureStep) => Centre(value.Closure(closureStep), (a, b) => a.Add(a.Subtract(b) / 2));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value, TimeSpan closureStep) => Centre(value.Closure(closureStep), (a, b) => a.Add(a.Subtract(b) / 2));

        public static bool HasGap(this Interval<int> value, Interval<int> other, int step) => DistanceTo(value.Closure(step), other) == step;

        /// <summary>
        /// Order does matter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int DistanceTo(this Interval<int> value, Interval<int> other)
        {
            return value.OverlapsWith(other)
                ? 0
                : other.Start.Value - value.End.Value;
        }

        private static Infinity<int> Calculate<T>(
            Interval<T> value,
            Func<T, T, int> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<int>()
                : value.IsEmpty() ? 0 : substract(value.End.Value, value.Start.Value);

        private static Infinity<double> CalculateEndStart<T>(
            Interval<T> value,
            Func<T, T, double> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<double>()
                : value.IsEmpty() ? 0 : substract(value.End.Value, value.Start.Value);


        private static Infinity<TimeSpan> CalculateEndStart<T>(
            Interval<T> value,
            Func<T, T, TimeSpan> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.Start.IsInfinite || value.End.IsInfinite
                ? new Infinity<TimeSpan>()
                : value.IsEmpty() ? TimeSpan.Zero : substract(value.End.Value, value.Start.Value);

        private static TResult? Centre<T, TResult>(Interval<T> value, Func<T, T, TResult> calculate)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            return value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : calculate(value.Start.Value, value.End.Value);
        }
    }
}
