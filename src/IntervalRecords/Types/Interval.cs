using IntervalRecords.Types;
using System.Text;
using Unbounded;

namespace IntervalRecords
{
    /// <summary>
    /// Represents an interval of values of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    public abstract record class Interval<T> : IComparable<Interval<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        private readonly Unbounded<T> _start;
        private readonly Unbounded<T> _end;

        /// <summary>
        /// Represents the start value of the interval.
        /// </summary>
        public Unbounded<T> Start
        {
            get => _start;
            init
            {
                _start = -value;
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
            }
        }

        public static Interval<T> Create(Unbounded<T> start, Unbounded<T> end, IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => new ClosedInterval<T>(start, end),
            IntervalType.ClosedOpen => new ClosedOpenInterval<T>(start, end),
            IntervalType.OpenClosed => new OpenClosedInterval<T>(start, end),
            IntervalType.Open => new OpenInterval<T>(end, end),
        };

        public static Interval<T> Empty(IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => new ClosedInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN),
            IntervalType.ClosedOpen => new ClosedOpenInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN),
            IntervalType.OpenClosed => new OpenClosedInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN),
            IntervalType.Open => new OpenInterval<T>(Unbounded<T>.NaN, Unbounded<T>.NaN),
        }; 

        /// <summary>
        /// Determines the interval type.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="value">The interval to determine the type of.</param>
        /// <returns>The interval type as an IntervalType enumeration value.</returns>
        public abstract IntervalType IntervalType { get; }

        /// <summary>
        /// Gets a value indicating whether the start of the interval is inclusive.
        /// </summary>
        public abstract bool StartInclusive { get; }

        /// <summary>
        /// Gets a value indicating whether the end of the interval is inclusive.
        /// </summary>
        public abstract bool EndInclusive { get; }

        /// <summary>
        /// Indicates whether the start value of the interval is less than or equal to its end value.
        /// </summary>
        public virtual bool IsValid => Start < End && !Start.IsNaN || Start.IsNaN && End.IsNaN;

        /// <summary>
        /// Indicates whether an interval is empty.
        /// </summary>
        /// <returns>True if the interval is Invalid or the interval is not <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal</returns>
        public virtual bool IsEmpty => !IsValid;

        /// <summary>
        /// Indicates whether an interval is Singleton.
        /// </summary>
        /// <returns>True if the interval is <see cref="IntervalType.Closed"/> and <see cref="Start"/> and <see cref="End"/> are equal.</returns>
        public virtual bool IsSingleton => false;

        /// <summary>
        /// Creates an interval.
        /// </summary>
        /// <param name="start">Represents the start value of the interval.</param>
        /// <param name="end">Represents the end value of the interval.</param>
        /// <param name="startInclusive">Indicates whether the start is inclusive.</param>
        /// <param name="endInclusive">Indicates whether the end is inclusive.</param>
        public Interval(Unbounded<T> start, Unbounded<T> end)
        {
            _start = -start;
            _end = +end;
        }

        /// <summary>
        /// Determines the bounded state of the interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="source">The interval to determine the bounded state of.</param>
        /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
        /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
        public BoundaryState GetBoundaryState()
            => (Start.State, End.State) switch
            {
                (UnboundedState.Finite, UnboundedState.Finite) => BoundaryState.Bounded,
                (UnboundedState.NegativeInfinity, UnboundedState.PositiveInfinity) => BoundaryState.Unbounded,
                (UnboundedState.NegativeInfinity, UnboundedState.Finite) => BoundaryState.RightBounded,
                (UnboundedState.Finite, UnboundedState.PositiveInfinity) => BoundaryState.LeftBounded,
                _ => throw new NotSupportedException()
            };

        /// <summary>
        /// Returns a boolean value indicating if the current interval overlaps with the other interval.
        /// </summary>
        /// <param name="other">The interval to check for overlapping with the current interval.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
        public abstract bool Overlaps(Interval<T> other);

        public abstract bool IsConnected(Interval<T> other);

        /// <summary>
        /// Returns a boolean value indicating if the current interval contains the specified value.
        /// </summary>
        /// <param name="value">The value to check if it is contained by the current interval</param>
        /// <returns></returns>
        public abstract bool Contains(Unbounded<T> value);

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
