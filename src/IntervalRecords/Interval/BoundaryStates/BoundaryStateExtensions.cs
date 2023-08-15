using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbounded;

namespace IntervalRecords;
public static class BoundaryStateExtensions
{
    /// <summary>
    /// Determines the bounded state of the interval.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to determine the bounded state of.</param>
    /// <returns>A value indicating whether the interval is bounded, left-bounded, right-bounded, or unbounded.</returns>
    /// <exception cref="NotSupportedException">Thrown when the start or end state of the interval is not finite or infinity.</exception>
    public static BoundaryState GetBoundaryState<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => (value.Start.State, value.End.State) switch
        {
            (UnboundedState.Finite, UnboundedState.Finite)
            or (UnboundedState.NaN, UnboundedState.NaN) => BoundaryState.Bounded,
            (UnboundedState.NegativeInfinity, UnboundedState.PositiveInfinity) => BoundaryState.Unbounded,
            (UnboundedState.NegativeInfinity, UnboundedState.Finite) => BoundaryState.RightBounded,
            (UnboundedState.Finite, UnboundedState.PositiveInfinity) => BoundaryState.LeftBounded,
            _ => throw new NotSupportedException()
        };

    /// <summary>
    /// Determines if the interval is half-bounded.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="source">The interval to check.</param>
    /// <returns>True if the interval is half-bounded.</returns>
    public static bool IsHalfBounded<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => value.GetBoundaryState() is BoundaryState.LeftBounded or BoundaryState.RightBounded;

    /// <summary>
    /// Determines if the interval is half-open.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="value">The interval to determine if it is half-open.</param>
    /// <returns>True if the interval is half-open.</returns>
    public static bool IsHalfOpen<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        => value.IntervalType is IntervalType.ClosedOpen or IntervalType.OpenClosed;

}
