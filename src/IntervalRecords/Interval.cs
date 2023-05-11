using System.Text;
using Unbounded;

namespace IntervalRecords
{
    /// <summary>
    /// Represents an interval of values of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    public record class Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly Unbounded<T> _start;
        private readonly Unbounded<T> _end;
        private readonly bool _startInclusive;
        private readonly bool _endInclusive;

        /// <summary>
        /// Represents the start value of the interval.
        /// </summary>
        public Unbounded<T> Start
        {
            get => _start;
            init
            {
                _start = -value;
                _startInclusive = !Start.IsNegativeInfinity && _startInclusive;
            }
        }

        /// <summary>
        /// Represents the end value of the interval.
        /// </summary>
        public Unbounded<T> End
        {
            get => _end;
            init
            {
                _end = +value;
                _endInclusive = !End.IsPositiveInfinity && _endInclusive;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the start of the interval is inclusive.
        /// </summary>
        public bool StartInclusive { get => _startInclusive; init { _startInclusive = !Start.IsNegativeInfinity && value; } }


        /// <summary>
        /// Gets a value indicating whether the end of the interval is inclusive.
        /// </summary>
        public bool EndInclusive { get => _endInclusive; init { _endInclusive = !End.IsPositiveInfinity && value; } }

        /// <summary>
        /// Indicates whether the end value of the interval is Greater than or equal to its start value.
        /// </summary>
        public bool IsValid => !Start.IsNaN || !End.IsNaN || End.CompareTo(Start) >= 0;

        /// <summary>
        /// Creates an unbounded interval equivalent to <see cref="Interval.All{T}"/>"/>
        /// </summary>
        public Interval()
            : this(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity, false, false)
        {
        }

        /// <summary>
        /// Creates an interval.
        /// </summary>
        /// <param name="start">Represents the start value of the interval.</param>
        /// <param name="end">Represents the end value of the interval.</param>
        /// <param name="startInclusive">Indicates whether the start is inclusive.</param>
        /// <param name="endInclusive">Indicates whether the end is inclusive.</param>
        public Interval(Unbounded<T> start, Unbounded<T> end, bool startInclusive, bool endInclusive)
        {
            _start = -start;
            _end = +end;
            _startInclusive = !_start.IsNegativeInfinity && startInclusive;
            _endInclusive = !_end.IsPositiveInfinity && endInclusive;
        }

        /// <summary>
        /// Indicates whether an interval is empty.
        /// </summary>
        /// <returns>True if the interval is Invalid or the interval is not <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal</returns>
        public bool IsEmpty() => !IsValid || (this.GetIntervalType() != IntervalType.Closed && Start == End);

        /// <summary>
        /// Indicates whether an interval is Singleton.
        /// </summary>
        /// <returns>True if the interval is <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal.</returns>
        public bool IsSingleton() => this.GetIntervalType() == IntervalType.Closed && Start == End;

        /// <summary>
        /// Returns a boolean value indicating if the current interval overlaps with the other interval.
        /// </summary>
        /// <param name="other">The interval to check for overlapping with the current interval.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
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
        public int CompareTo(Interval<T>? other)
        {
            if (other == null || this > other)
            {
                return 1;
            }
            if (this < other)
            {
                return -1;
            }
            return 0;
        }

        public static bool operator >(Interval<T> left, Interval<T> right)
        {
            int compareEnd = left.CompareEnd(right);
            return compareEnd == 1 || compareEnd == 0 && left.CompareStart(right) == -1;
        }

        public static bool operator <(Interval<T> left, Interval<T> right)
        {
            int compareEnd = left.CompareEnd(right);
            return compareEnd == -1 || compareEnd == 0 && left.CompareStart(right) == 1;
        }

        public static bool operator >=(Interval<T> left, Interval<T> right) => left == right || left > right;

        public static bool operator <=(Interval<T> left, Interval<T> right) => left == right || left < right;

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

        public void Deconstruct(out Unbounded<T> start, out Unbounded<T> end, out bool startInclusive, out bool endInclusive)
        {
            start = Start;
            end = End;
            startInclusive = StartInclusive;
            endInclusive = EndInclusive;
        }
    }
}
