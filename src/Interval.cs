using InfinityComparable;
using IntervalRecord.Enums;
using IntervalRecord.Extensions;
using System.Linq.Expressions;
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

        public Infinity<T> Start { get => _start; init => _start = new Infinity<T>((T?)value, false); }
        public Infinity<T> End { get => _end; init => _end = new Infinity<T>((T?)value, true); }
        public bool StartInclusive { get => _start.Finite is not null && _startInclusive; init => _startInclusive = value; }
        public bool EndInclusive { get => _end.Finite is not null && _endInclusive; init => _endInclusive = value; }

        public Interval()
            : this(null, null, false, false)
        {
        }

        public Interval(T? start, T? end)
            : this(start, end, false, false)
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

        public bool IsEmpty() => Start.Finite is not null && End.Finite is not null
                && Start.Finite!.Value.Equals(End.Finite!.Value) && !IsClosed();

        public bool IsSingleton() => !IsUnBounded() && Start.Finite!.Value.Equals(End.Finite!.Value) && IsClosed();

        public bool IsUnBounded() => Start.IsInfinite && End.IsInfinite;

        public bool IsHalfBounded() => IsLeftBounded() || IsRightBounded();

        public bool IsLeftBounded() => !Start.IsInfinite && End.IsInfinite;

        public bool IsRightBounded() => Start.IsInfinite && !End.IsInfinite;

        public bool OverlapsWith(Interval<T> other) => !this.IsBefore(other) && !this.IsAfter(other);



        public static explicit operator string(Interval<T> interval) => interval.ToString();
        public static explicit operator Interval<T>(string str) => IntervalParser.Parse<T>(str);
        public static implicit operator Interval<T>?(string str)
        {
            IntervalParser.TryParse<T>(str, out var result);
            return result;
        }

        public static bool operator >(Interval<T> a, Interval<T> b)
        {
            return a.End.CompareTo(b.End) == 1
                || a.End.CompareTo(b.End) == 0 && a.Start.CompareTo(b.Start) == 1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            return a.End.CompareTo(b.End) == -1
                || a.End.CompareTo(b.End) == 0 && a.Start.CompareTo(b.Start) == -1;
        }

        public static bool operator >=(Interval<T> a, Interval<T> b)
        {
            return a == b || a > b;
        }

        public static bool operator <=(Interval<T> a, Interval<T> b)
        {
            return a == b || a < b;
        }

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

        public void Deconstruct(out T? start, out T? end, out bool startInclusive, out bool endInclusive)
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

        public static Infinity<int> Length(this Interval<int> value) => (Infinity<int>)Convert.ChangeType(Length(value.Closure(1), (a, b) => a - b), typeof(Infinity<int>));
        public static Infinity<int> Length(this Interval<DateOnly> value) => (Infinity<int>)Convert.ChangeType(Length(value.ClosureByDays(1), (a, b) => a.DayNumber - b.DayNumber), typeof(Infinity<int>));
        
        /// <summary>
        /// Length of interval
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval <see cref="Closure(Interval{DateTime}, TimeSpan)"/> with maximum precision of 1 tick</param>
        /// <returns></returns>
        public static Infinity<TimeSpan> Length(this Interval<DateTime> value) => Length(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.Subtract(b));


        /// <summary>
        /// Length of interval
        /// </summary>
        /// <param name="value">The interval value will be transformed to a closed interval <see cref="Closure(Interval{DateTimeOffset}, TimeSpan)"/> with maximum precision of 1 tick</param>
        /// <returns></returns>
        public static Infinity<TimeSpan> Length(this Interval<DateTimeOffset> value) => Length(value.Closure(TimeSpan.FromMilliseconds(1)), (a, b) => a.Subtract(b));

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

        public static Interval<int> Interior(this Interval<int> value, int stepSize)
            => Interior(value, x => x + stepSize, x => x - stepSize);
        public static Interval<DateOnly> Interior(this Interval<DateOnly> value, int days)
            => Interior(value, x => x.AddDays(days), x => x.AddDays(-days));
        public static Interval<DateTime> Interior(this Interval<DateTime> value, TimeSpan stepSize)
            => Interior(value, x => x.Add(stepSize), x => x.Subtract(stepSize));
        public static Interval<DateTimeOffset> Interior(this Interval<DateTimeOffset> value, TimeSpan stepSize)
            => Interior(value, x => x.Add(stepSize), x => x.Subtract(stepSize));


        public static Interval<int> Closure(this Interval<int> value, int stepSize)
            => Closure(value, x => x + stepSize, x => x - stepSize);
        public static Interval<DateOnly> ClosureByDays(this Interval<DateOnly> value, int days)
            => Closure(value, x => x.AddDays(days), x => x.AddDays(-days));
        public static Interval<DateOnly> ClosureByMonths(this Interval<DateOnly> value, int days)
            => Closure(value, x => x.AddDays(days), x => x.AddDays(-days));
        public static Interval<DateOnly> ClosureByYears(this Interval<DateOnly> value, int days)
            => Closure(value, x => x.AddDays(days), x => x.AddDays(-days));
        public static Interval<DateTime> Closure(this Interval<DateTime> value, TimeSpan stepSize)
            => Closure(value, x => x.Add(stepSize), x => x.Subtract(stepSize));
        public static Interval<DateTimeOffset> Closure(this Interval<DateTimeOffset> value, TimeSpan stepSize)
            => Closure(value, x => x.Add(stepSize), x => x.Subtract(stepSize));

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
            return substract(value.End.Finite!.Value, value.Start.Finite!.Value);
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
            return substract(value.End.Finite!.Value, value.Start.Finite!.Value);
        }
        
        private static TResult? Calculate<T, TResult>(Interval<T> value, Func<T, T, TResult> calculate)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TResult : struct
        {
            return value.Start.IsInfinite || value.End.IsInfinite || value.IsEmpty()
                ? null
                : calculate(value.Start.Finite!.Value, value.End.Finite!.Value);
        }

        private static Interval<T> Closure<T>(
            Interval<T> value,
            Func<T, T> add,
            Func<T, T> substract)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value with
            {
                Start = value.StartInclusive || value.Start.IsInfinite ? value.Start : add(value.Start.Finite!.Value),
                End = value.EndInclusive || value.End.IsInfinite ? value.End : substract(value.End.Finite!.Value),
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
            var result = value with
            {
                Start = value.StartInclusive ? substract(value.Start.Finite!.Value) : value.Start,
                End = value.EndInclusive ? add(value.End.Finite!.Value) : value.End,
                StartInclusive = false,
                EndInclusive = false
            };
            return result.IsValid() ? result : value;
        }
    }
}
