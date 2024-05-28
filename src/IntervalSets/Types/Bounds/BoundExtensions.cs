namespace IntervalSets.Types;
public static class BoundExtensions
{
    public static bool IsOpen(this Bound bound) => bound is Bound.Open;

    public static bool IsClosed(this Bound bound) => bound is Bound.Closed;

    public static bool IsUnbounded(this Bound bound) => bound is Bound.Unbounded;

}
