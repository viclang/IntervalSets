namespace IntervalSet.Types;
public static class IntervalTypeFactory
{

    /// <summary>
    /// Creates an IntervalType based on the provided start and end Bound values.
    /// </summary>
    /// <param name="start">The start boundary of the interval.</param>
    /// <param name="end">The end boundary of the interval.</param>
    /// <returns>An IntervalType representing the combination of the start and end bounds.</returns>
    public static IntervalType Create(Bound start, Bound end)
    {
        return (IntervalType)(EncodeStartBound(start) | (byte)end);
    }

    private static int EncodeStartBound(Bound start) => (byte)start << 1;
}
