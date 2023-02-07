using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    /// <summary>
    /// Specifies the type of interval, whether it is closed, open-closed, closed-open, or open.
    /// </summary>
    public enum IntervalType : byte
    {
        Closed = 0,
        ClosedOpen = 1,
        OpenClosed = 2,
        Open = 3,
    }

    public static class IntervalTypeExtensions
    {
        /// <summary>
        /// Converts the specified IntervalType to a tuple of two booleans representing the interval's endpoints being inclusive or exclusive.
        /// </summary>
        /// <param name="intervalType">The interval type to be converted.</param>
        /// <returns>A tuple of two booleans representing the interval's endpoints being inclusive or exclusive.</returns>
        /// <exception cref="NotSupportedException">Thrown when the specified interval type is not a recognized value.</exception>
        [Pure]
        public static (bool, bool) ToTuple(this IntervalType intervalType) => intervalType switch
        {
            IntervalType.Closed => (true, true),
            IntervalType.ClosedOpen => (true, false),
            IntervalType.OpenClosed => (false, true),
            IntervalType.Open => (false, false),
            _ => throw new NotSupportedException()
        };
    }

    public static partial class Interval
    {
        /// <summary>
        /// Determines the interval type.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="value">The interval to determine the type of.</param>
        /// <returns>The interval type as an IntervalType enumeration value.</returns>
        [Pure]
        public static IntervalType GetIntervalType<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (value.StartInclusive, value.EndInclusive) switch
            {
                (true, true) => IntervalType.Closed,
                (true, false) => IntervalType.ClosedOpen,
                (false, true) => IntervalType.OpenClosed,
                (false, false) => IntervalType.Open,
            };

        /// <summary>
        /// Determines if the interval is half-open.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="value">The interval to determine if it is half-open.</param>
        /// <returns>True if the interval is half-open.</returns>
        [Pure]
        public static bool IsHalfOpen<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.StartInclusive && !value.EndInclusive || !value.StartInclusive && value.EndInclusive;

    }
}
