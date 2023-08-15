using Unbounded;

namespace IntervalRecords
{
    /// <summary>
    /// Represents an interval of values of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of values represented in the interval.</typeparam>
    public abstract partial record Interval<T> : IComparable<Interval<T>>
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
        public virtual bool IsEmpty => !IsValid || Start == End;

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
        public Interval(T? start, T? end)
        {
            _start = start.ToNegativeInfinity();
            _end = end.ToPositiveInfinity();
        }

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

        public static Interval<T> Unbounded(IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => ClosedInterval<T>.Unbounded,
            IntervalType.ClosedOpen => ClosedOpenInterval<T>.Unbounded,
            IntervalType.OpenClosed => OpenClosedInterval<T>.Unbounded,
            IntervalType.Open => OpenInterval<T>.Unbounded,
            _ => throw new NotImplementedException(),
        };

        public static Interval<T> Empty(IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => ClosedInterval<T>.Empty,
            IntervalType.ClosedOpen => ClosedOpenInterval<T>.Empty,
            IntervalType.OpenClosed => OpenClosedInterval<T>.Empty,
            IntervalType.Open => OpenInterval<T>.Empty,
            _ => throw new NotImplementedException(),
        };


        /// <summary>
        /// Returns a boolean value indicating if the current interval contains the specified value.
        /// </summary>
        /// <param name="value">The value to check if it is contained by the current interval</param>
        /// <returns></returns>
        public abstract bool Contains(T value);

        /// <summary>
        /// Returns a boolean value indicating if the current interval overlaps with the other interval.
        /// </summary>
        /// <param name="other">The interval to check for overlapping with the current interval.</param>
        /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
        public abstract bool Overlaps(Interval<T> other);

        public abstract bool IsConnected(Interval<T> other);


        /// <summary>
        /// Compares the start of two intervals.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
        public abstract int CompareStart(Interval<T> other);

        /// <summary>
        /// Compares the end of two intervals.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        public abstract int CompareEnd(Interval<T> other);

        /// <summary>
        /// Compares the start of the first interval with the end of the second interval.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        public abstract int CompareStartToEnd(Interval<T> other);

        /// <summary>
        /// Compares the end of the first interval with the start of the second interval.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        public abstract int CompareEndToStart(Interval<T> other);

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
            return compareEnd == 1 || (compareEnd == 0 && left.CompareStart(right) == -1);
        }

        public static bool operator <(Interval<T> left, Interval<T> right)
        {
            int compareEnd = left.CompareEnd(right);
            return compareEnd == -1 || (compareEnd == 0 && left.CompareStart(right) == 1);
        }

        public static bool operator >=(Interval<T> left, Interval<T> right) => left == right || left > right;

        public static bool operator <=(Interval<T> left, Interval<T> right) => left == right || left < right;

        public void Deconstruct(out Unbounded<T> start, out Unbounded<T> end, out bool startInclusive, out bool endInclusive)
        {
            start = Start;
            end = End;
            startInclusive = StartInclusive;
            endInclusive = EndInclusive;
        }
    }
}
