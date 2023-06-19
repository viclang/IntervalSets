using IntervalRecords.Types;
using Unbounded;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Creates an empty interval with default values for start and end
        /// </summary>
        /// <typeparam name="T">The type of value to store in the interval.</typeparam>
        /// <returns>A new empty interval with default values for start and end</returns>
        public static OpenInterval<T> Empty<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(default(T), default(T));

        /// <summary>
        /// Creates an unbounded interval that contains all the values of type <see cref="{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to store in the interval.</typeparam>
        /// <returns>A new unbounded interval that contains every value of type <see cref="{T}"/>.</returns>
        public static OpenInterval<T> All<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, Unbounded<T>.PositiveInfinity);

        /// <summary>
        /// Creates a singleton interval that contains only one value.
        /// </summary>
        /// <typeparam name="T">The type of value to store in the interval.</typeparam>
        /// <param name="value">The value to store in the interval.</param>
        /// <returns>A new singleton interval that contains only the specified value.</returns>
        public static Interval<T> Singleton<T>(T value) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedInterval<T>(value, value);

        /// <summary>
        /// Creates an interval with a specified start, end, and interval type
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="start">The start value of the interval.</param>
        /// <param name="end">The end value of the interval.</param>
        /// <param name="intervalType">The type of interval</param>
        /// <returns>A new interval with the specified start, end, and interval type</returns>
        public static Interval<T> WithIntervalType<T>(Unbounded<T> start, Unbounded<T> end, IntervalType intervalType) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => intervalType switch
        {
            IntervalType.Closed => new ClosedInterval<T>(start.GetFiniteOrDefault(), end.GetFiniteOrDefault()),
            IntervalType.ClosedOpen => new ClosedOpenInterval<T>(start, end),
            IntervalType.OpenClosed => new OpenClosedInterval<T>(start, end),
            IntervalType.Open => new OpenInterval<T>(start, end),
            _ => throw new NotSupportedException()
        };

        /// <summary>
        /// Creates an open interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open interval with the specified start and end</returns>
        public static OpenInterval<T> Open<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(start, end);

        /// <summary>
        /// Creates a closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new closed interval with the specified start and end</returns>
        public static ClosedInterval<T> Closed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedInterval<T>(start, end);

        /// <summary>
        /// Creates an open-closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open-closed interval with the specified start and end</returns>
        public static OpenClosedInterval<T> OpenClosed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenClosedInterval<T>(start, end);

        /// <summary>
        /// Creates an interval with a closed start and open end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The value to use as the start of the interval</param>
        /// <param name="end">The value to use as the end of the interval</param>
        public static ClosedOpenInterval<T> ClosedOpen<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedOpenInterval<T>(start, end);

        public static Interval<T> CreateInterval<T>(
            Unbounded<T> start,
            Unbounded<T> end,
            bool startInclusive,
            bool endInclusive) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (startInclusive, endInclusive) switch
        {
            (true, true) => new ClosedInterval<T>(start.GetFiniteOrDefault(), end.GetFiniteOrDefault()),
            (false, true) => new OpenClosedInterval<T>(start, end),
            (true, false) => new ClosedInterval<T>(start.GetFiniteOrDefault(), end.GetFiniteOrDefault()),
            (false, false) => new OpenClosedInterval<T>(start, end)
        };

        /// <summary>
        /// Creates an interval with values greater than the specified start value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <returns>A new interval that includes all values greater than the specified start value.</returns>
        public static OpenInterval<T> GreaterThan<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

        /// <summary>
        /// Creates an interval with values greater than or equal to the specified start value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <returns>A new interval that includes all values greater than or equal to the specified start value.</returns>
        public static ClosedOpenInterval<T> AtLeast<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new ClosedOpenInterval<T>(start, Unbounded<T>.PositiveInfinity);

        /// <summary>
        /// Creates an interval with values less than the specified end value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new interval that includes all values less than the specified end value.</returns>
        public static Interval<T> LessThan<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenInterval<T>(Unbounded<T>.NegativeInfinity, end);

        /// <summary>
        /// Creates an interval with values less than or equal to the specified end value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new interval that includes all values less than or equal to the specified end value.</returns>
        public static OpenClosedInterval<T> AtMost<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new OpenClosedInterval<T>(Unbounded<T>.NegativeInfinity, end);
    }
}
