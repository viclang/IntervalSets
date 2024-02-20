namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Determines the interval type.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="value">The interval to determine the type of.</param>
    /// <returns>The interval type as an IntervalType enumeration value.</returns>
    public static BoundaryType GetBoundaryType<T>(this Interval<T> value) where T : struct, IComparable<T>, ISpanParsable<T>
        => (BoundaryType)((value.StartInclusive ? 1 : 0) | (value.EndInclusive ? 2 : 0));

    /// <summary>
    /// Determines if the interval is half-open.
    /// </summary>
    /// <typeparam name="T">The type of the interval endpoints.</typeparam>
    /// <param name="value">The interval to determine if it is half-open.</param>
    /// <returns>True if the interval is half-open.</returns>
    public static bool IsHalfOpen<T>(this Interval<T> value)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
        => value.GetBoundaryType() is BoundaryType.ClosedOpen or BoundaryType.OpenClosed;

    /// <summary>
    /// Converts the specified IntervalType to a tuple of two booleans representing the interval's endpoints being inclusive or exclusive.
    /// </summary>
    /// <param name="intervalType">The interval type to be converted.</param>
    /// <returns>A tuple of two booleans representing the interval's endpoints being inclusive or exclusive.</returns>
    /// <exception cref="NotSupportedException">Thrown when the specified interval type is not a recognized value.</exception>
    public static (bool, bool) ToTuple(this BoundaryType intervalType)
    {
        bool startInclusive = (intervalType is BoundaryType.Closed or BoundaryType.ClosedOpen);
        bool endInclusive = (intervalType is BoundaryType.Closed or BoundaryType.OpenClosed);
        return (startInclusive, endInclusive);
    }
}
