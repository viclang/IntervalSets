using IntervalRecords.Types;
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
        /// Determines if the interval is half-bounded.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="source">The interval to check.</param>
        /// <returns>True if the interval is half-bounded.</returns>
        public bool IsHalfBounded() => GetBoundaryState() is BoundaryState.LeftBounded or BoundaryState.RightBounded;

        /// <summary>
        /// Determines if the interval is half-open.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="value">The interval to determine if it is half-open.</param>
        /// <returns>True if the interval is half-open.</returns>
        public bool IsHalfOpen()
            => IntervalType is IntervalType.ClosedOpen or IntervalType.OpenClosed;

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
        /// Determines the bounded state of the interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="source">The interval to determine the bounded state of.</param>
        /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
        /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
        public BoundaryState GetBoundaryState()
            => (Start.State, End.State) switch
            {
                (UnboundedState.Finite, UnboundedState.Finite)
                or (UnboundedState.NaN, UnboundedState.NaN) => BoundaryState.Bounded,
                (UnboundedState.NegativeInfinity, UnboundedState.PositiveInfinity) => BoundaryState.Unbounded,
                (UnboundedState.NegativeInfinity, UnboundedState.Finite) => BoundaryState.RightBounded,
                (UnboundedState.Finite, UnboundedState.PositiveInfinity) => BoundaryState.LeftBounded,
                _ => throw new NotSupportedException()
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
        /// Determines interval overlapping relation between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        public IntervalOverlapping GetOverlap(Interval<T> other) => new ValueTuple<int, int>(CompareStart(other), CompareEnd(other)) switch
        {
            (0, 0) => IntervalOverlapping.Equal,
            (0, -1) => IntervalOverlapping.Starts,
            (1, -1) => IntervalOverlapping.ContainedBy,
            (1, 0) => IntervalOverlapping.Finishes,
            (-1, 0) => IntervalOverlapping.FinishedBy,
            (-1, 1) => IntervalOverlapping.Contains,
            (0, 1) => IntervalOverlapping.StartedBy,
            (-1, -1) => CompareEndToStart(other),
            (1, 1) => CompareStartToEnd(other),
            (_, _) => throw new NotSupportedException()
        };

        /// <summary>
        /// Compares the start of two intervals.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
        protected virtual int CompareStart(Interval<T> other)
        {
            return Start == other.Start ? StartInclusive.CompareTo(other.StartInclusive) : Start.CompareTo(other.Start);
        }

        /// <summary>
        /// Compares the end of two intervals.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        protected virtual int CompareEnd(Interval<T> other)
        {
            return End == other.End ? EndInclusive.CompareTo(other.EndInclusive) : End.CompareTo(other.End);
        }

        /// <summary>
        /// Compares the start of the first interval with the end of the second interval.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        public virtual IntervalOverlapping CompareStartToEnd(Interval<T> other)
        {
            return Start == other.End && (!StartInclusive || !other.EndInclusive)
                ? IntervalOverlapping.After
                : (IntervalOverlapping)Start.CompareTo(other.End) + (int)IntervalOverlapping.MetBy;
        }

        /// <summary>
        /// Compares the end of the first interval with the start of the second interval.
        /// </summary>
        /// <param name="other">The other interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        public virtual IntervalOverlapping CompareEndToStart(Interval<T> other)
        {
            var result = End.CompareTo(other.Start);
            return result == 0 && (!EndInclusive || !other.StartInclusive)
                ? IntervalOverlapping.Before
                : (IntervalOverlapping)result + (int)IntervalOverlapping.Meets;
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

        public void Deconstruct(out Unbounded<T> start, out Unbounded<T> end, out bool startInclusive, out bool endInclusive)
        {
            start = Start;
            end = End;
            startInclusive = StartInclusive;
            endInclusive = EndInclusive;
        }

        /// <summary>
        /// Computes the union of two intervals if they overlap.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval to be unioned.</param>
        /// <param name="other">The second interval to be unioned.</param>
        /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
        public Interval<T>? Union(Interval<T> other)
            => IsConnected(other)
                ? Hull(other)
                : null;

        /// <summary>
        /// Computes the smallest interval that contains both input intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="other">The second interval to compute the hull of.</param>
        /// <returns>The smallest interval that contains both input intervals.</returns>
        public Interval<T> Hull(Interval<T> other)
        {
            var minByStart = MinBy(other, i => i.Start);
            var maxByEnd = MaxBy(other, i => i.End);

            var startInclusive = Start == other.Start
                ? StartInclusive || other.StartInclusive
                : minByStart.StartInclusive;

            var endInclusive = End == other.End
                ? EndInclusive || other.EndInclusive
                : maxByEnd.EndInclusive;

            return Interval.Create(minByStart.Start, maxByEnd.End, startInclusive, endInclusive);
        }

        /// <summary>
        /// Returns the interval that is greater than or equal to the other interval, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
        /// <param name="other">The second interval to compare.</param>
        /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
        /// <returns>The interval that is greater than or equal to the other interval based on the comparison of the selected values.</returns>
        public Interval<T> MaxBy<TProperty>(Interval<T> other, Func<Interval<T>, TProperty> selector)
            where TProperty : IComparable<TProperty>
            => selector(this).CompareTo(selector(other)) >= 0 ? this : other;

        /// <summary>
        /// Returns the minimum interval between two intervals, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
        /// <param name="other">The second interval to compare.</param>
        /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
        /// <returns>The interval that is less than or equal to the other interval based on the comparison of the selected values.</returns>
        public Interval<T> MinBy<TProperty>(Interval<T> other, Func<Interval<T>, TProperty> selector)
            where TProperty : IComparable<TProperty>
            => selector(this).CompareTo(selector(other)) <= 0 ? this : other;

        /// <summary>
        /// Returns the gap between two intervals, or null if the two intervals overlap.
        /// </summary>
        /// <param name="other">The second interval.</param>
        /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
        public Interval<T>? Gap(Interval<T> other)
        {
            if (Start > other.End || (Start == other.End && !StartInclusive && !other.EndInclusive))
            {
                return Interval.Create(other.End, Start, !other.EndInclusive, !StartInclusive);
            }
            if (End < other.Start || (End == other.Start && !EndInclusive && !other.StartInclusive))
            {
                return Interval.Create(End, other.Start, !EndInclusive, !other.StartInclusive);
            }
            return null;
        }
    }
}
