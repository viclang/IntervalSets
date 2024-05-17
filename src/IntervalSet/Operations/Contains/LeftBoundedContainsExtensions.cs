using IntervalSet.Types;
using IntervalSet.Types.Unbounded;

namespace IntervalSet.Operations;
public static class LeftBoundedContainsExtensions
{
    public static bool Contains<T>(this ILeftBoundedInterval<T> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) is var comparison
            && (comparison < 0 || comparison == 0 && interval.StartBound.IsClosed());
    }

    public static bool Contains<T, L>(this TypedLeftBoundedInterval<T, Open> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
    {
        return interval.Start.CompareTo(other) < 0;
    }

    public static bool Contains<T, L>(this TypedLeftBoundedInterval<T, Closed> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBound
    {
        return interval.Start.CompareTo(other) <= 0;
    }
}
