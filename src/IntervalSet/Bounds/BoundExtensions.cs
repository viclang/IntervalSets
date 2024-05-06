namespace IntervalSet.Bounds;

public static class BoundExtensions
{
    public static bool IsClosed(this Bound bound)
    {
        return bound is Bound.Closed;
    }

    public static bool IsOpen(this Bound bound)
    {
        return bound is Bound.Open;
    }
}
