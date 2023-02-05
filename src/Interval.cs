using Infinity;
using IntervalRecord.BoundaryComparers;
using IntervalRecord.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public readonly record struct Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Infinity<T> _start { get; init; } = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Infinity<T> _end { get; init; } = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _startInclusive { get; init; } = false;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _endInclusive { get; init; } = false;

        public T? Start { get => _start.IsInfinite ? null : _start.Value; init => _start = value.ToNegativeInfinity(); }

        public T? End { get => _end.IsInfinite ? null : _end.Value; init => _end = value.ToPositiveInfinity(); }
        public bool StartInclusive { get => !_start.IsInfinite && _startInclusive; init => _startInclusive = value; }
        public bool EndInclusive { get => !_end.IsInfinite && _endInclusive; init => _endInclusive = value; }


        public Interval(T? start, T? end)
            : this(start, end, (false, false))
        {
        }

        public Interval(T? start, T? end, BoundaryType boundaryType)
            : this(start, end, BoundaryTypeMapper.ToTuple(boundaryType))
        {
        }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
            : this(start, end,(startInclusive, endInclusive))
        {
        }

        private Interval(T? start, T? end, (bool start, bool end) inclusive)
        {
            Start = start;
            End = end;
            StartInclusive = inclusive.start;
            EndInclusive = inclusive.end;
        }

        public bool IsValid()
        {
            return Validate(out var _);
        }

        public bool Validate(out string? message)
        {
            if (!_start.IsInfinite && !_end.IsInfinite && _startInclusive && _endInclusive
                    && _end.CompareTo(_start) == -1)
            {
                message = "The End parameter must be greater or equal to the Start parameter";
                return false;
            }

            if (!_start.IsInfinite && !_end.IsInfinite && !(_start.Equals(_end)
                && (!_startInclusive || !_endInclusive))
                && _end.CompareTo(_start) <= 0)
            {
                message = "The End parameter must be greater than the Start parameter";
                return false;
            }
            message = null;
            return true;
        }

        public Interval<T> ValidateAndThrow()
        {
            if (!Validate(out var message))
            {
                throw new ArgumentOutOfRangeException("End", message);
            }
            return this;
        }

        public BoundaryType GetIntervalType()
        {
            return BoundaryTypeMapper.ToType(StartInclusive, EndInclusive);
        }

        public bool IsEmpty()
        {
            if (!IsBounded())
            {
                return false;
            }

            return Start!.Value.Equals(End!.Value)
                && (!StartInclusive || !EndInclusive);
        }

        public bool IsBounded()
        {
            return !_start.IsInfinite && !_end.IsInfinite;
        }

        public bool IsHalfBounded()
        {
            return IsLeftBounded() || IsRightBounded();
        }

        public bool IsLeftBounded()
        {
            return !_start.IsInfinite && _end.IsInfinite;
        }

        public bool IsRightBounded()
        {
            return _start.IsInfinite && !_end.IsInfinite;
        }

        public bool IsConnected(Interval<T> other)
        {
            if (this == other)
            {
                return true;
            }
            return IsBounded()
                    && other.IsBounded()
                    && End!.Value.CompareTo(other.Start!.Value) == 1
                    && other.End!.Value.CompareTo(Start!.Value) == 1
                || IsRightBounded() && other.IsLeftBounded()
                    && End!.Value.Equals(other.Start!.Value) && (EndInclusive || other.StartInclusive)
                || IsLeftBounded() && other.IsRightBounded()
                    && Start!.Value.Equals(other.End) && (StartInclusive || other.EndInclusive);
        }

        public bool OverlapsWith(Interval<T> other)
        {
            return !this.IsBefore(other) && !this.IsAfter(other);
        }

        public static bool operator >(Interval<T> a, Interval<T> b)
        {
            return (a.CompareStart(b) == 1 || a.CompareStart(b) == -1) && a.CompareEnd(b) == 1
                || a.CompareStart(b) == 0 && a.CompareEnd(b) == 1
                || a.CompareEnd(b) == 0 && a.CompareStart(b) == 1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            return (a.CompareStart(b) == 1 || a.CompareStart(b) == -1) && a.CompareEnd(b) == -1
                || a.CompareStart(b) == 0 && a.CompareEnd(b) == -1
                || a.CompareEnd(b) == 0 && a.CompareStart(b) == -1;
        }

        public static bool operator >=(Interval<T> a, Interval<T> b)
        {
            return a == b || a > b;
        }

        public static bool operator <=(Interval<T> a, Interval<T> b)
        {
            return a == b || a < b;
        }

        public override string? ToString()
        {
            return (_startInclusive ? "[" : "(")
                + _start
                + ", "
                + _end
                + (EndInclusive ? "]" : ")");
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
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(default(T), default(T), false, false);
        }

        public static Interval<T> All<T>()
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(null, null, false, false);
        }

        public static Interval<T> Singleton<T>(T value)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(value, value, true, true);
        }

        public static Interval<T> Open<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, end, false, false);
        }

        public static Interval<T> Closed<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, end, true, true);
        }

        public static Interval<T> OpenClosed<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, end, false, true);
        }

        public static Interval<T> ClosedOpen<T>(T start, T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, end, true, false);
        }

        public static Interval<T> GreaterThan<T>(T start)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, null, false, false);
        }

        public static Interval<T> AtLeast<T>(T start)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(start, null, true, false);
        }

        public static Interval<T> LessThan<T>(T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(null, end, false, false);
        }

        public static Interval<T> AtMost<T>(T end)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return new Interval<T>(null, end, false, true);
        }

        public static bool TryParse<T>(string s, out Interval<T> result)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return IntervalParser.TryParse<T>(s, out result);
        }

        public static Interval<T> Parse<T>(string s)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return IntervalParser.ParseSingle<T>(s);
        }

        public static IEnumerable<Interval<T>> ParseAll<T>(string s)
            where T : struct, IEquatable<T>, IComparable<T>
        {
            return IntervalParser.ParseAll<T>(s);
        }
    }
}
