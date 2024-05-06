using Intervals.Bounds;
using Intervals.Types;

namespace Intervals.Operations;
public static class RightBoundedContainsExtensions
{
    public static bool Contains<T>(this IRightBoundedInterval<T> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return interval.End.CompareTo(other) is var comparison
            && (comparison < 0 || comparison == 0 && interval.EndBound.IsClosed());
    }

    public static bool Contains<T, R>(this TypedRightBoundedInterval<T, Open> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
    {
        return interval.End.CompareTo(other) < 0;
    }

    public static bool Contains<T, R>(this TypedRightBoundedInterval<T, Closed> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where R : struct, IBound
    {
        return interval.End.CompareTo(other) <= 0;
    }
}
