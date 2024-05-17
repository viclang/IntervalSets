using IntervalSet.Types;

namespace IntervalSet.Operations;
public static class BoundedContainsExtensions
{
    public static bool Contains<T>(this IBoundedInterval<T> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = interval.Start.CompareTo(other);
        var endComparison = other.CompareTo(interval.End);

        return startComparison < 0 && endComparison < 0
            || startComparison == 0 && interval.StartBound.IsClosed()
            || endComparison == 0 && interval.EndBound.IsClosed();
    }

    public static bool Contains<T>(this TypedBoundedInterval<T, Open, Open> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) < 0
            && other.CompareTo(interval.End) < 0;
    }

    public static bool Contains<T>(this TypedBoundedInterval<T, Closed, Open> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) <= 0
            && other.CompareTo(interval.End) < 0;
    }

    public static bool Contains<T>(this TypedBoundedInterval<T, Open, Closed> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) < 0
            && other.CompareTo(interval.End) <= 0;
    }

    public static bool Contains<T>(this TypedBoundedInterval<T, Closed, Closed> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) <= 0
            && other.CompareTo(interval.End) <= 0;
    }
}
