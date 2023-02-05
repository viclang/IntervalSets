using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public readonly record struct Interval<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        public T? Start { get; init; }
        public T? End { get; init; }
        public bool StartInclusive { get; init; }
        public bool EndInclusive { get; init; }

        public Interval(T? start, T? end, bool startInclusive, bool endInclusive)
        {
            Start = start;
            End = end;
            StartInclusive = start is null ? false : startInclusive;
            EndInclusive = end is null ? false : endInclusive;

            if (Start is not null && End is not null
                    && StartInclusive && EndInclusive
                    && End.Value.CompareTo(Start.Value) == -1)
            {
                throw new ArgumentOutOfRangeException("The end parameter must be greater or equal to the start parameter");
            }

            if (Start is not null && End is not null
                    && (!StartInclusive || !EndInclusive)
                    && End.Value.CompareTo(Start.Value) <= 0)
            {
                throw new ArgumentOutOfRangeException("The end parameter must be greater than the start parameter");
            }
        }

        public IntervalType GetIntervalType()
        {
            return IntervalTypeMapper.ToType(StartInclusive, EndInclusive);
        }

        public bool IsConnected(Interval<T> other)
        {
            if (this == other)
            {
                return true;
            }

            return (Start is not null && End is not null
                    && other.Start is not null && other.End is not null
                    && End.Value.CompareTo(other.Start.Value) == 1
                    && other.End.Value.CompareTo(Start.Value) == 1)
                || End is not null && other.Start is not null
                    && End.Value.Equals(other.Start.Value) && (EndInclusive || other.StartInclusive)
                || Start is not null && other.End is not null
                    && Start.Value.Equals(other.End) && (StartInclusive || other.EndInclusive);
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
                return End.Value.CompareTo(other.Start.Value) <= 0;
            }

            return End.Value.CompareTo(other.Start.Value) == -1;
        }

        public bool IsAfter(Interval<T> other)
        {
            if (Start is null || other.End is null)
            {
                return false;
            }

            if (!StartInclusive || !other.EndInclusive)
            {
                return Start.Value.CompareTo(other.End.Value) >= 0;
            }

            return Start.Value.CompareTo(other.End.Value) == 1;
        }

        public static bool operator >(Interval<T> a, Interval<T> b)
        {
            if(a == b)
            {
                return false;
            }

            if (a.Start is null && a.End is null
                && (b.Start is not null || b.End is not null))
            {
                return true;
            }

            if ((a.Start is not null || a.End is not null)
                && b.Start is null && b.End is null)
            {
                return false;
            }

            if((a.Start is null || a.End is null)
                && b.Start is not null && b.End is not null)
            {
                return true;
            }

            if (a.Start is not null && a.End is not null
                && (b.Start is null || b.End is null))
            {
                return true;
            }

            return a.Start!.Value.CompareTo(b.Start!.Value) == 1 && a.End!.Value.CompareTo(b.End!.Value) == 1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            if (a == b)
            {
                return false;
            }

            if (a.Start is null && a.End is null
                && (b.Start is not null || b.End is not null))
            {
                return false;
            }

            if ((a.Start is not null || a.End is not null)
                && b.Start is null && b.End is null)
            {
                return true;
            }

            if (a.Start is not null && a.End is not null
                && (b.Start is null || b.End is null))
            {
                return false;
            }

            if ((a.Start is null || a.End is null)
                && b.Start is not null && b.End is not null)
            {
                return true;
            }

            return a.Start!.Value.CompareTo(b.Start!.Value) == -1 && a.End!.Value.CompareTo(b.End!.Value) == -1;
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
            var start = Start is null
                ? "(-∞"
                : StartInclusive
                    ? "[" + Start.Value
                    : "(" + Start.Value;

            var end = End is null
                ? "+∞)"
                : EndInclusive
                    ? End.Value + "]"
                    : End.Value + ")";

            return string.Join(", ", start, end);
        }
    }

    public static class Interval
    {
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

        public static Interval<T> All<T>()
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return new Interval<T>(null, null, false, false);
        }

        public static Interval<T> FromString<T>(string interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return IntervalStringReader.FromString<T>(interval);
        }
    }

}
