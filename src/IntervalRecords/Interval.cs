using InfinityComparable;
using System.Diagnostics.Contracts;
using System.Text;

namespace IntervalRecords
{
    /// <summary>
    /// Represents an interval of values of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    public record class Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly Infinity<T> start;
        private readonly Infinity<T> end;
        private readonly bool startInclusive;
        private readonly bool endInclusive;
        
        /// <summary>
        /// Represents the start value of the interval.
        /// </summary>
        public Infinity<T> Start
        {
            get => start;
            init
            {
                start = -value;
                startInclusive = !Start.IsInfinity && startInclusive;
            }
        }

        /// <summary>
        /// Represents the end value of the interval.
        /// </summary>
        public Infinity<T> End
        {
            get => end;
            init
            {
                end = +value;
                endInclusive = !End.IsInfinity && endInclusive;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the start of the interval is inclusive.
        /// </summary>
        public bool StartInclusive { get => startInclusive; init { startInclusive = !Start.IsInfinity && value; } }


        /// <summary>
        /// Gets a value indicating whether the end of the interval is inclusive.
        /// </summary>
        public bool EndInclusive { get => endInclusive; init { endInclusive = !End.IsInfinity && value; } }

        /// <summary>
        /// Indicates whether the end value of the interval is Greater than or equal to its start value.
        /// </summary>
        [Pure]
        public bool IsValid => End.CompareTo(Start) >= 0 || Start.IsNaN || End.IsNaN;

        /// <summary>
        /// Creates an unbounded interval equivalent to <see cref="Interval.All{T}"/>"/>
        /// </summary>
        [Pure]
        public Interval()
            : this(Infinity<T>.NegativeInfinity, Infinity<T>.PositiveInfinity, false, false)
        {
        }

        /// <summary>
        /// Creates an interval.
        /// </summary>
        /// <param name="start">Represents the start value of the interval.</param>
        /// <param name="end">Represents the end value of the interval.</param>
        /// <param name="startInclusive">Indicates whether the start is inclusive.</param>
        /// <param name="endInclusive">Indicates whether the end is inclusive.</param>
        [Pure]
        public Interval(Infinity<T> start, Infinity<T> end, bool startInclusive, bool endInclusive)
        {
            this.start = -start;
            this.end = +end;
            this.startInclusive = !this.start.IsInfinity && startInclusive;
            this.endInclusive = !this.end.IsInfinity && endInclusive;
        }

        /// <summary>
        /// Indicates whether an interval is empty.
        /// </summary>
        /// <returns>True if the interval is Invalid or the interval is not <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal</returns>
        [Pure]
        public bool IsEmpty() => !IsValid || (this.GetIntervalType() != IntervalType.Closed && Start == End);

        /// <summary>
        /// Indicates whether an interval is Singleton.
        /// </summary>
        /// <returns>True if the interval is <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal.</returns>
        [Pure]
        public bool IsSingleton() => this.GetIntervalType() == IntervalType.Closed && Start == End;

        /// <summary>
        /// Returns a boolean value indicating if the current interval overlaps with the other interval.
        /// </summary>
        /// <param name="other">The interval to check for overlapping with the current interval.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
        [Pure]
        public bool Overlaps(Interval<T> other, bool includeHalfOpen = false)
        {
            bool notBefore = this.CompareEndToStart(other, includeHalfOpen) != -1;
            bool notAfter = this.CompareStartToEnd(other, includeHalfOpen) != 1;
            return notBefore && notAfter;
        }

        /// <summary>
        /// Returns a boolean value indicating if the current interval contains the specified value.
        /// </summary>
        /// <param name="value">The value to check if it is contained by the current interval</param>
        /// <returns></returns>
        [Pure]
        public bool Contains(T value)
        {
            bool startsBeforeValue = Start.CompareTo(value) == -1;
            bool startsAtValue = Start.CompareTo(value) == 0 && StartInclusive;
            bool endsAfterValue = End.CompareTo(value) == 1;
            bool endsAtValue = End.CompareTo(value) == 0 && EndInclusive;
            return (startsBeforeValue || startsAtValue) && (endsAfterValue || endsAtValue);
        }

        /// <summary>
        /// This method compares the current interval with another interval and returns an integer value indicating the relative order of the intervals.
        /// </summary>
        /// <param name="other">The interval to compare with the current interval.</param>
        /// <returns>1 if the current interval is greater than the other interval, -1 if the current interval is less than the other interval, and 0 if the intervals are equal.</returns>
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
            int compareEnd = left.CompareEnd(right);
            return compareEnd == 1 || compareEnd == 0 && left.CompareStart(right) == -1;
        }

        [Pure]
        public static bool operator <(Interval<T> left, Interval<T> right)
        {
            int compareEnd = left.CompareEnd(right);
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
