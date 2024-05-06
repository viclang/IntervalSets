namespace Intervals.Bounds;
public static class BoundPairFactory
{

    /// <summary>
    /// Creates an IntervalType based on the provided start and end Bound values.
    /// </summary>
    /// <param name="start">The start boundary of the interval.</param>
    /// <param name="end">The end boundary of the interval.</param>
    /// <returns>An IntervalType representing the combination of the start and end bounds.</returns>
    public static BoundPair Create(Bound start, Bound end)
    {
        return (BoundPair)(EncodeStartBound(start) | (byte)end);
    }

    private static int EncodeStartBound(Bound start) => (byte)start << 1;
}
