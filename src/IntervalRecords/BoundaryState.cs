using Unbounded;

namespace IntervalRecords
{
    /// <summary>
    /// Specifies the bounded state of an interval.
    /// </summary>
    public enum BoundaryState : byte
    {
        /// <summary>
        /// Both endpoints are bounded.
        /// </summary>
        Bounded = 0,
        /// <summary>
        /// Both endpoints are unbounded.
        /// </summary>
        Unbounded = 1,
        /// <summary>
        /// The left endpoint is bounded, the right endpoint is unbounded.
        /// </summary>
        LeftBounded = 2,
        /// <summary>
        /// The right endpoint is bounded, the left endpoint is unbounded.
        /// </summary>
        RightBounded = 3,
    }


    public static partial class Interval
    {
        /// <summary>
        /// Determines the bounded state of the interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="source">The interval to determine the bounded state of.</param>
        /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
        /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
        public static BoundaryState GetBoundaryState<T>(this Interval<T> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return (source.Start.State, source.End.State) switch
            {
                (UnboundedState.Finite, UnboundedState.Finite) => BoundaryState.Bounded,
                (UnboundedState.NegativeInfinity, UnboundedState.PositiveInfinity) => BoundaryState.Unbounded,
                (UnboundedState.NegativeInfinity, UnboundedState.Finite) => BoundaryState.RightBounded,
                (UnboundedState.Finite, UnboundedState.PositiveInfinity) => BoundaryState.LeftBounded,
                _ => throw new NotSupportedException()
            };
        }

        /// <summary>
        /// Determines if the interval is half-bounded.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="source">The interval to check.</param>
        /// <returns>True if the interval is half-bounded.</returns>
        public static bool IsHalfBounded<T>(this Interval<T> source)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => source.GetBoundaryState() is BoundaryState.LeftBounded or BoundaryState.RightBounded;
    }
}
