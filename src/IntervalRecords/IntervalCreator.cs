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
        public static Interval<T> Empty<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(default(T), default(T), false, false);

        /// <summary>
        /// Creates an unbounded interval that contains all the values of type <see cref="{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to store in the interval.</typeparam>
        /// <returns>A new unbounded interval that contains every value of type <see cref="{T}"/>.</returns>
        public static Interval<T> All<T>() where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new();

        /// <summary>
        /// Creates a singleton interval that contains only one value.
        /// </summary>
        /// <typeparam name="T">The type of value to store in the interval.</typeparam>
        /// <param name="value">The value to store in the interval.</param>
        /// <returns>A new singleton interval that contains only the specified value.</returns>
        public static Interval<T> Singleton<T>(T value) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(value, value, true, true);

        /// <summary>
        /// Creates an interval with a specified start, end, and interval type
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="start">The start value of the interval.</param>
        /// <param name="end">The end value of the interval.</param>
        /// <param name="intervalType">The type of interval</param>
        /// <returns>A new interval with the specified start, end, and interval type</returns>
        public static Interval<T> WithIntervalType<T>(Unbounded<T> start, Unbounded<T> end, IntervalType intervalType) where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var (startInclusive, endInclusive) = intervalType.ToTuple();
            return new(start, end, startInclusive, endInclusive);
        }

        /// <summary>
        /// Creates an open interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval.</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open interval with the specified start and end</returns>
        public static Interval<T> Open<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, false, false);

        /// <summary>
        /// Creates a closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new closed interval with the specified start and end</returns>
        public static Interval<T> Closed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, true, true);

        /// <summary>
        /// Creates an open-closed interval with a specified start and end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new open-closed interval with the specified start and end</returns>
        public static Interval<T> OpenClosed<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, false, true);

        /// <summary>
        /// Creates an interval with a closed start and open end
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The value to use as the start of the interval</param>
        /// <param name="end">The value to use as the end of the interval</param>
        public static Interval<T> ClosedOpen<T>(T start, T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, end, true, false);

        /// <summary>
        /// Creates an interval with values greater than the specified start value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <returns>A new interval that includes all values greater than the specified start value.</returns>
        public static Interval<T> GreaterThan<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, Unbounded<T>.PositiveInfinity, false, false);

        /// <summary>
        /// Creates an interval with values greater than or equal to the specified start value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="start">The start value of the interval</param>
        /// <returns>A new interval that includes all values greater than or equal to the specified start value.</returns>
        public static Interval<T> AtLeast<T>(T start) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(start, Unbounded<T>.PositiveInfinity, true, false);

        /// <summary>
        /// Creates an interval with values less than the specified end value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new interval that includes all values less than the specified end value.</returns>
        public static Interval<T> LessThan<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(Unbounded<T>.NegativeInfinity, end, false, false);

        /// <summary>
        /// Creates an interval with values less than or equal to the specified end value.
        /// </summary>
        /// <typeparam name="T">The type of values to store in the interval</typeparam>
        /// <param name="end">The end value of the interval</param>
        /// <returns>A new interval that includes all values less than or equal to the specified end value.</returns>
        public static Interval<T> AtMost<T>(T end) where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => new(Unbounded<T>.NegativeInfinity, end, false, true);
    }
}
