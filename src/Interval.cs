using IntervalRecord.BoundaryComparers;
using IntervalRecord.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public readonly record struct Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>
    {
        private static bool _startInclusive;
        private static bool _endInclusive;
        public T? Start { get; init; }
        public T? End { get; init; }
        public bool StartInclusive { get => Start is not null && _startInclusive; init => _startInclusive = value; }
        public bool EndInclusive { get => End is not null && _endInclusive; init => _endInclusive = value; }


        public Interval(T? start, T? end)
        {
            Start = start;
            End = end;
        }

        public Interval(T? start, T? end, bool startInclusive)
        {
            Start = start;
            End = end;
            StartInclusive = startInclusive;
        }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
        {
            Start = start;
            End = end;
            StartInclusive = startInclusive;
            EndInclusive = endInclusive;

            if (IsBounded() && StartInclusive && EndInclusive
                    && End!.Value.CompareTo(Start!.Value) == -1)
            {
                throw new ArgumentOutOfRangeException("The end parameter must be greater or equal to the start parameter");
            }

            if (IsBounded() && !IsEmpty()
                && (!StartInclusive || !EndInclusive)
                && End!.Value.CompareTo(Start!.Value) <= 0)
            {
                throw new ArgumentOutOfRangeException("The end parameter must be greater than the start parameter");
            }
        }

        public BoundaryType GetIntervalType()
        {
            return IntervalTypeMapper.ToType(StartInclusive, EndInclusive);
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
            return Start is not null && End is not null;
        }

        public bool IsHalfBounded()
        {
            return IsLeftBounded() || IsRightBounded();
        }

        public bool IsLeftBounded()
        {
            return Start is not null && End is null;
        }

        public bool IsRightBounded()
        {
            return Start is null && End is not null;
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
            return !IsBefore(other) && !IsAfter(other);
        }

        public bool IsBefore(Interval<T> other)
        {
            if (End is null || other.Start is null)
            {
                return false;
            }
            if (!EndInclusive || !other.StartInclusive)
            {
                return End!.Value.CompareTo(other.Start!.Value) <= 0;
            }
            return End!.Value.CompareTo(other.Start!.Value) == -1;
        }

        public bool IsAfter(Interval<T> other)
        {
            if (Start is null || other.End is null)
            {
                return false;
            }
            if (!StartInclusive || !other.EndInclusive)
            {
                return Start!.Value.CompareTo(other.End!.Value) >= 0;
            }
            return Start!.Value.CompareTo(other.End!.Value) == 1;
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
            return (StartInclusive ? "[" : "(")
                + (Start is null ? "-∞" : Start!.Value.ToString())
                + ", "
                + (End is null ? "+∞" : End.Value.ToString())
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
