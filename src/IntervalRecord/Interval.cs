using InfinityComparable;
using System.Diagnostics.Contracts;
using System.Text;

namespace IntervalRecord
{
    public readonly record struct Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly Infinity<T> start;
        private readonly Infinity<T> end;
        private readonly bool startInclusive;
        private readonly bool endInclusive;

        public Infinity<T> Start
        {
            get => start;
            init
            {
                start = -value;
                startInclusive = !Start.IsInfinity && startInclusive;
            }
        }

        public Infinity<T> End
        {
            get => end;
            init
            {
                end = +value;
                endInclusive = !End.IsInfinity && endInclusive;
            }
        }

        public bool StartInclusive { get => startInclusive; init { startInclusive = !Start.IsInfinity && value; } }

        public bool EndInclusive { get => endInclusive; init { endInclusive = !End.IsInfinity && value; } }

        [Pure]
        public bool IsValid => Start.IsInfinity || End.IsInfinity || End.CompareTo(Start) >= 0;

        [Pure]
        public Interval()
            : this(Infinity<T>.NegativeInfinity, Infinity<T>.PositiveInfinity, false, false)
        {
        }

        [Pure]
        public Interval(Infinity<T> start, Infinity<T> end, bool startInclusive, bool endInclusive)
        {
            this.start = -start;
            this.end = +end;
            this.startInclusive = !this.start.IsInfinity && startInclusive;
            this.endInclusive = !this.end.IsInfinity && endInclusive;
        }

        [Pure]
        public bool IsEmpty() => !IsValid || (this.GetIntervalType() != IntervalType.Closed && Start == End);

        [Pure]
        public bool IsSingleton() => this.GetIntervalType() == IntervalType.Closed && Start == End;

        [Pure]
        public bool Overlaps(Interval<T> other, bool includeHalfOpen = false)
            => this.GetOverlappingState(other, includeHalfOpen) is not OverlappingState.Before and not OverlappingState.After;

        [Pure]
        public bool Contains(T other)
        {
            return StartInclusive
                ? Start.CompareTo(other) <= 0
                : Start.CompareTo(other) == -1 && EndInclusive
                    ? End.CompareTo(other) >= 0
                    : End.CompareTo(other) == 1;
        }


        [Pure]
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

        [Pure]
        public static bool operator >(Interval<T> left, Interval<T> right)
        {
            var compareEnd = left.CompareEnd(right);
            return compareEnd == 1 || compareEnd == 0 && left.CompareStart(right) == -1;
        }

        [Pure]
        public static bool operator <(Interval<T> left, Interval<T> right)
        {
            var compareEnd = left.CompareEnd(right);
            return compareEnd == -1 || compareEnd == 0 && left.CompareStart(right) == 1;
        }

        [Pure]
        public static bool operator >=(Interval<T> left, Interval<T> right) => left == right || left > right;

        [Pure]
        public static bool operator <=(Interval<T> left, Interval<T> right) => left == right || left < right;

        [Pure]
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

        [Pure]
        public void Deconstruct(out Infinity<T> start, out Infinity<T> end, out bool startInclusive, out bool endInclusive)
        {
            start = Start;
            end = End;
            startInclusive = StartInclusive;
            endInclusive = EndInclusive;
        }
    }
}
