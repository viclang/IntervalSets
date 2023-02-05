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

        public bool IsClosed()
        {
            return StartInclusive && EndInclusive;
        }

        public BoundaryType GetIntervalType()
        {
            return BoundaryTypeMapper.ToType(StartInclusive, EndInclusive);
        }

        public bool IsEmpty()
        {
            return !IsUnBounded() && Start.Finite!.Value.Equals(End.Finite!.Value) && !IsClosed();
        }

        public bool IsSingleton()
        {
            return !IsUnBounded() && Start.Finite!.Value.Equals(End.Finite!.Value) && IsClosed();
        }

        public bool IsUnBounded()
        {
            return Start.IsInfinite && End.IsInfinite;
        }

        public bool IsHalfBounded()
        {
            return IsLeftBounded() || IsRightBounded();
        }

        public bool IsLeftBounded()
        {
            return !Start.IsInfinite && End.IsInfinite;
        }

        public bool IsRightBounded()
        {
            return Start.IsInfinite && !End.IsInfinite;
        }

        public bool OverlapsWith(Interval<T> other)
        {
            return !this.IsBefore(other) && !this.IsAfter(other);
        }


        public static explicit operator string(Interval<T> interval) => interval.ToString();
        public static explicit operator Interval<T>(string str) => IntervalParser.Parse<T>(str);
        public static implicit operator Interval<T>?(string str) => IntervalParser.TryParse<T>(str, out var result) ? result : null;

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
    }
}
