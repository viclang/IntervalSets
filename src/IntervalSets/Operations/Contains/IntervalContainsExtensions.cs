using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalContainsExtensions
{
    public static bool Contains<T>(this Interval<T> interval, T value)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = interval.StartBound.IsUnbounded() ? -1 : interval.Start.CompareTo(value);
        var endComparison = interval.EndBound.IsUnbounded() ? -1 : value.CompareTo(interval.End);

        return startComparison < 0 && endComparison < 0
            || startComparison == 0 && interval.StartBound.IsClosed()
            || endComparison == 0 && interval.EndBound.IsClosed();
    }

    public static bool Contains<T>(this Interval<T, Open, Open> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) < 0
            && value.CompareTo(interval.End) < 0;
    }

    public static bool Contains<T>(this Interval<T, Closed, Open> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) <= 0
            && value.CompareTo(interval.End) < 0;
    }

    public static bool Contains<T>(this Interval<T, Open, Closed> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) < 0
            && value.CompareTo(interval.End) <= 0;
    }

    public static bool Contains<T>(this Interval<T, Closed, Closed> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) <= 0
            && value.CompareTo(interval.End) <= 0;
    }

    public static bool Contains<T>(this Interval<T, Open, Unbounded> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) < 0;
    }

    public static bool Contains<T>(this Interval<T, Closed, Unbounded> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(value) <= 0;
    }

    public static bool Contains<T>(this Interval<T, Unbounded, Open> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return value.CompareTo(interval.End) < 0;
    }

    public static bool Contains<T>(this Interval<T, Unbounded, Closed> interval, T value)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return value.CompareTo(interval.End) <= 0;
    }

    public static bool Contains<T>(this Interval<T, Unbounded, Unbounded> _, T __)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return true;
    }
}
