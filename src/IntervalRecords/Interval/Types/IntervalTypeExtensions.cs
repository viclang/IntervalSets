namespace IntervalRecords;
public static class IntervalTypeExtensions
{
    /// <summary>
    /// Converts the specified IntervalType to a tuple of two booleans representing the interval's endpoints being inclusive or exclusive.
    /// </summary>
    /// <param name="intervalType">The interval type to be converted.</param>
    /// <returns>A tuple of two booleans representing the interval's endpoints being inclusive or exclusive.</returns>
    /// <exception cref="NotSupportedException">Thrown when the specified interval type is not a recognized value.</exception>
    public static (bool, bool) ToTuple(this IntervalType intervalType) => intervalType switch
    {
        IntervalType.Closed => (true, true),
        IntervalType.ClosedOpen => (true, false),
        IntervalType.OpenClosed => (false, true),
        IntervalType.Open => (false, false),
        _ => throw new NotSupportedException()
    };
}
