using InfinityComparable;
using IntervalRecord.Enums;
using IntervalRecord.Extensions;
using System.Text;

namespace IntervalRecord
{
    public readonly record struct Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly Infinity<T> _start;
        private readonly Infinity<T> _end;
        private readonly bool _startInclusive;
        private readonly bool _endInclusive;

        public Infinity<T> Start { get => _start; init => _start = new Infinity<T>(value, false); }
        public Infinity<T> End { get => _end; init => _end = new Infinity<T>(value, true); }
        public bool StartInclusive { get => !_start.IsInfinite && _startInclusive; init => _startInclusive = value; }
        public bool EndInclusive { get => !_end.IsInfinite && _endInclusive; init => _endInclusive = value; }

        public Interval()
            : this(null, null, false, false)
        {
        }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
        {
            _start = new Infinity<T>(start, false);
            _end = new Infinity<T>(end, true);
            _startInclusive = startInclusive;
            _endInclusive = endInclusive;
        }

        public bool IsClosed() => StartInclusive && EndInclusive;

        public BoundaryType GetIntervalType()
        {
            return BoundaryTypeMapper.ToType(StartInclusive, EndInclusive);
        }

        public bool IsEmpty() => !Start.IsInfinite && !End.IsInfinite
                && Start.Value.Equals(End.Value) && !IsClosed();

        public bool IsSingleton() => !IsUnBounded() && Start.Value.Equals(End.Value) && IsClosed();

        public bool IsUnBounded() => Start.IsInfinite && End.IsInfinite;

        public bool IsHalfBounded() => IsLeftBounded() || IsRightBounded();

        public bool IsLeftBounded() => !Start.IsInfinite && End.IsInfinite;

        public bool IsRightBounded() => Start.IsInfinite && !End.IsInfinite;

        public bool IsConnected(Interval<T> other) => !this.IsBefore(other) || !this.IsAfter(other);
        public bool OverlapsWith(Interval<T> other) => !this.IsExclusiveBefore(other) && !this.IsExclusiveAfter(other);

        public static explicit operator string(Interval<T> interval) => interval.ToString();
        public static explicit operator Interval<T>(string str) => IntervalParser.Parse<T>(str);
        public static implicit operator Interval<T>?(string str)
        {
            IntervalParser.TryParse<T>(str, out var result);
            return result;
        }

        public static bool operator >(Interval<T> a, Interval<T> b)
            => a.End.CompareTo(b.End) == 1
                || a.End.CompareTo(b.End) == 0 && a.Start.CompareTo(b.Start) == 1;
        public static bool operator <(Interval<T> a, Interval<T> b)
            => a.End.CompareTo(b.End) == -1
                || a.End.CompareTo(b.End) == 0 && a.Start.CompareTo(b.Start) == -1;
        public static bool operator >=(Interval<T> a, Interval<T> b) => a == b || a > b;
        public static bool operator <=(Interval<T> a, Interval<T> b) => a == b || a < b;
        public static Interval<T> operator &(Interval<T> a, Interval<T> b) => a.Intersect(b);
        public static Interval<T> operator |(Interval<T> a, Interval<T> b) => a.Union(b);

        public int CompareTo(Interval<T> other)
        {
            if (this > other)
            {
                return 1;
            }
            if (this < other)
            {
                return -1;
            }
            return 0;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append(StartInclusive ? "[" : "(")
                .Append(Start)
                .Append(", ")
                .Append(End)
                .Append(EndInclusive ? "]" : ")")
                .ToString();
        }

        public void Deconstruct(out Infinity<T> start, out Infinity<T> end, out bool startInclusive, out bool endInclusive)
        {
            start = Start;
            end = End;
            startInclusive = StartInclusive;
            endInclusive = EndInclusive;
        }
    }

    public static class Interval
    {
        public static Interval<T> Empty<T>()
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(default(T), default(T), false, false);
        }

        public static Interval<T> All<T>()
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>();
        }

        public static Interval<T> Singleton<T>(T value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(value, value, true, true);
        }

        public static Interval<T> Open<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, false, false);
        }

        public static Interval<T> Closed<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, true, true);
        }

        public static Interval<T> OpenClosed<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, false, true);
        }

        public static Interval<T> ClosedOpen<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, end, true, false);
        }

        public static Interval<T> GreaterThan<T>(T start)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, null, false, false);
        }

        public static Interval<T> AtLeast<T>(T start)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(start, null, true, false);
        }

        public static Interval<T> LessThan<T>(T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(null, end, false, false);
        }

        public static Interval<T> AtMost<T>(T end)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(null, end, false, true);
        }

        public static bool TryParse<T>(string s, out Interval<T>? result)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return IntervalParser.TryParse<T>(s, out result);
        }

        public static Interval<T> Parse<T>(string s)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return IntervalParser.Parse<T>(s);
        }

        public static IEnumerable<Interval<T>> ParseAll<T>(string s)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return IntervalParser.ParseAll<T>(s);
        }

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
            if(selector(a).CompareTo(selector(b)) == -1)
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


        public static Infinity<double> Length(this Interval<int> value) => Length(value.Closure(1), (a, b) => a - b);
        public static Infinity<double> Length(this Interval<DateOnly> value) => Length(value.ClosureByDays(1), (a, b) => a.DayNumber - b.DayNumber);
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value) => Length(value.Closure(TimeSpan.FromDays(1)), (a, b) => a.Subtract(b));
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value) => Length(value.Closure(TimeSpan.FromDays(1)), (a, b) => a.Subtract(b));

        public static double? Radius(this Interval<int> value) => Calculate(value.Closure(1), (a, b) => (a - b) / 2);
        public static double? Radius(this Interval<DateOnly> value) => Calculate(value.ClosureByDays(1), (a, b) => (a.DayNumber - b.DayNumber) / 2);

        /// <summary>
        /// Radius
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval <see cref="Closure(Interval{DateTime}, TimeSpan)"/></param>
        /// <returns></returns>
        public static TimeSpan? Radius(this Interval<DateTime> value) => Calculate(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.Subtract(b)/2);
        /// <summary>
        /// Radius
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval before measurement <see cref="Closure(Interval{DateTimeOffset}, TimeSpan)"/></param>
        /// <returns></returns>
        public static TimeSpan? Radius(this Interval<DateTimeOffset> value) => Calculate(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.Subtract(b) / 2);

        public static double? Centre(this Interval<int> value) => Calculate(value.Closure(1), (a, b) => (a + b) / 2);
        public static DateOnly? Centre(this Interval<DateOnly> value) => Calculate(value.ClosureByDays(1), (a, b) => a.AddDays((int)Math.Round((double)(a.DayNumber + b.DayNumber) / 2)));
        public static DateTime? Centre(this Interval<DateTime> value) => Calculate(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.AddTicks(a.AddTicks(b.Ticks).Ticks / 2));
        public static DateTimeOffset? Centre(this Interval<DateTimeOffset> value) => Calculate(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.AddTicks(a.AddTicks(b.Ticks).Ticks / 2));

        public static bool HasGap(this Interval<int> value, Interval<int> other, int step) => DistanceTo(value, other) == step;

        /// <summary>
        /// Order does matter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static int DistanceTo(this Interval<int> value, Interval<int> other)
            => value.IsConnected(other)
                ? 0
                : other.Start.Value - value.End.Value;

        public static Interval<int> Interior(this Interval<int> value, int step)
            => Interior(value, x => x + step, x => x - step);
        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, int step)
            => Interior(value, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan step)
            => Interior(value, x => x.Add(step), x => x.Subtract(step));
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan step)
            => Interior(value, x => x.Add(step), x => x.Subtract(step));

        public static Interval<int> Closure(this Interval<int> value, int step)
            => Closure(value, x => x + step, x => x - step);

        public static Interval<DateOnly> ClosureByDays(this Interval<DateOnly> value, int step)
            => Closure(value, x => x.AddDays(step), x => x.AddDays(-step));

        public static Interval<DateOnly> ClosureByMonths(this Interval<DateOnly> value, int step)
            => Closure(value, x => x.AddDays(step), x => x.AddDays(-step));

        public static Interval<DateOnly> ClosureByYears(this Interval<DateOnly> value, int step)
            => Closure(value, x => x.AddDays(step), x => x.AddDays(-step));
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan step)
            => Closure(value, x => x.Add(step), x => x.Subtract(step));
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan step)
            => Closure(value, x => x.Add(step), x => x.Subtract(step));

        private static Infinity<double> Length<T>(
            Interval<T> value,
            Func<T, T, double> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.Start.IsInfinite || value.End.IsInfinite)
            {
                return new Infinity<double>();
            }
            if (value.IsEmpty())
            {
                return 0;
            }
            return substract(value.End.Value, value.Start.Value);
        }

        private static Infinity<TimeSpan> Length<T>(
            Interval<T> value,
            Func<T, T, TimeSpan> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.Start.IsInfinite || value.End.IsInfinite)
            {
                return new Infinity<TimeSpan>();
            }
            if (value.IsEmpty())
            {
                return new Infinity<TimeSpan>(TimeSpan.Zero, true);
            }
            return substract(value.End.Value, value.Start.Value);
        }
        
        private static TResult? Calculate<T, TResult>(Interval<T> value, Func<T, T, TResult> calculate)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            return value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : calculate(value.Start.Value, value.End.Value);
        }



        private static Interval<T> Closure<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (value.StartInclusive && value.EndInclusive)
            {
                return value;
            }
            var result = value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Value),
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Value),
                StartInclusive = true,
                EndInclusive = true
            };
            return result.IsValid() ? result : value;
        }

        private static Interval<T> Interior<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!value.StartInclusive && !value.EndInclusive)
            {
                return value;
            }
            var result = value with
            {
                Start = value.StartInclusive ? substract(value.Start.Value) : value.Start,
                End = value.EndInclusive ? add(value.End.Value) : value.End,
                StartInclusive = false,
                EndInclusive = false
            };
            return result.IsValid() ? result : value;
        }
    }
}
