using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class ComplementContainsExtensions
{
    public static bool Contains<T>(this IComplementInterval<T> interval, T other)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = interval.Start.CompareTo(other);
        var endComparison = other.CompareTo(interval.End);

        return startComparison > 0 && endComparison > 0
            || startComparison == 0 && interval.StartBound.IsOpen()
            || endComparison == 0 && interval.EndBound.IsOpen();
    }

    public static bool Contains<T>(this ComplementInterval<T, Open, Open> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) >= 0
            && other.CompareTo(interval.End) >= 0;
    }

    public static bool Contains<T>(this ComplementInterval<T, Closed, Closed> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) > 0
            && other.CompareTo(interval.End) > 0;
    }

    public static bool Contains<T>(this ComplementInterval<T, Closed, Open> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) > 0
            && other.CompareTo(interval.End) >= 0;
    }

    public static bool Contains<T>(this ComplementInterval<T, Open, Closed> interval, T other)
        where T : IComparable<T>, ISpanParsable<T>
    {
        return interval.Start.CompareTo(other) >= 0
            && other.CompareTo(interval.End) > 0;
    }
}
