using IntervalSet.Types;
using IntervalSet.Types.Unbounded;

namespace IntervalSet.Operations;
public static class LeftBoundedOverlapsExtensions
{
    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this ILeftBoundedInterval<T> left, ILeftBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return true;
    }

    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this ILeftBoundedInterval<T> left, IRightBoundedInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = left.Start.CompareTo(right.End);

        return startComparison < 0
            || startComparison == 0 && left.StartBound.IsClosed() && right.EndBound.IsClosed();
    }
}
