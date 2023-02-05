using InfinityComparable;
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
        public bool StartInclusive { get => _startInclusive; init { _startInclusive = !Start.IsInfinite && value; } }
        public bool EndInclusive { get => _endInclusive; init { _endInclusive = !End.IsInfinite && value; } }

        public Interval()
            : this(null, null, false, false)
        {
        }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
        {
            _start = new Infinity<T>(start, false);
            _end = new Infinity<T>(end, true);
            _startInclusive = !_start.IsInfinite && startInclusive;
            _endInclusive = !_end.IsInfinite && endInclusive;
        }

        public bool IsConnected(Interval<T> other) => !this.IsBefore(other) && !this.IsAfter(other);
        public bool OverlapsWith(Interval<T> other) => !this.IsBefore(other) && !this.IsAfter(other);

        public static explicit operator string(Interval<T> interval) => interval.ToString();
        public static explicit operator Interval<T>(string str) => Interval.Parse<T>(str);
        public static implicit operator Interval<T>?(string str)
        {
            Interval.TryParse<T>(str, out var result);
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
}
