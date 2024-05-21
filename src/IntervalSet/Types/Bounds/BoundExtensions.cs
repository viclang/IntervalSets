namespace IntervalSet.Types;

public static class BoundExtensions
{
    public static bool IsOpen(this Bound bound)
    {
        return bound is Bound.Open;
    }

    public static bool IsClosed(this Bound bound)
    {
        return bound is Bound.Closed;
    }

    public static bool IsUnbounded(this Bound bound)
    {
        return bound is Bound.Unbounded;
    }

}
