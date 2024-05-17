using IntervalSet.Types;
using IntervalSet.Types.Unbounded;

namespace IntervalSet.Operations;
public static class RightBoundedOverlapsExtensions
{
    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IRightBoundedInterval<T> left, IRightBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return true;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IRightBoundedInterval<T> left, ILeftBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var endComparison = right.Start.CompareTo(left.End);

        return endComparison < 0
            || endComparison == 0 && left.EndBound.IsClosed() && right.StartBound.IsClosed();
    }
}
