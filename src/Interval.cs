using IntervalRecord.BoundaryComparers;
using IntervalRecord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    public readonly record struct Interval<T> : IComparable<Interval<T>>, IComparable
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private static readonly IComparer<Interval<T>> _startComparer = new StartComparer<T>();
        private static readonly IComparer<Interval<T>> _endComparer = new EndComparer<T>();
        private static readonly IComparer<Interval<T>> _startEndComparer = new StartComparer<T>();
        private static readonly IComparer<Interval<T>> _endStartComparer = new EndComparer<T>();

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
            return (IsBounded()
                    && other.IsBounded()
                    && End!.Value.CompareTo(other.Start!.Value) == 1
                    && other.End!.Value.CompareTo(Start!.Value) == 1)
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
            if (End is null && other.Start is null)
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
            if (Start is null && other.End is null)
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
            return (_startComparer.Compare(a, b) == 1 || _startComparer.Compare(a, b) == -1) && _endComparer.Compare(a, b) == 1
                || _startComparer.Compare(a, b) == 0 && _endComparer.Compare(a, b) == 1
                || _endComparer.Compare(a, b) == 0 && _startComparer.Compare(a, b) == -1;
        }

        public static bool operator <(Interval<T> a, Interval<T> b)
        {
            return (_startComparer.Compare(a, b) == 1 || _startComparer.Compare(a, b) == -1) && _endComparer.Compare(a, b) == -1
                || _startComparer.Compare(a, b) == 0 && _endComparer.Compare(a, b) == -1
                || _endComparer.Compare(a, b) == 0 && _startComparer.Compare(a, b) == 1;
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
        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
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
}
