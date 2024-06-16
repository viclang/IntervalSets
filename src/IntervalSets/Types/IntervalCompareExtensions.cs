namespace IntervalSets.Types;
public static class IntervalCompareExtensions
{

    public static int CompareStart<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return (left.StartBound.IsUnbounded(), right.StartBound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => -1,
            (false, true) => 1,
            (false, false) =>
                left.Start.CompareTo(right.Start) switch
                {
                    0 => left.StartBound.CompareTo(right.StartBound),
                    var comparison => comparison
                }
        };
    }

    public static int CompareEnd<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        return (left.EndBound.IsUnbounded(), right.EndBound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => 1,
            (false, true) => -1,
            (false, false) =>
                left.End.CompareTo(right.End) switch
                {
                    0 => left.EndBound.CompareTo(right.EndBound),
                    var comparison => comparison
                }
        };
    }

    /// <summary>
    /// Compares the start of the first interval with the end of the second interval.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    public static int CompareStartToEnd<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (left.StartBound.IsUnbounded() || right.EndBound.IsUnbounded())
        {
            return -1;
        }

        int comparison = left.Start.CompareTo(right.End);
        if (comparison == 0 && (left.StartBound.IsOpen() || right.EndBound.IsOpen()))
        {
            return 1;
        }
        return comparison;
    }

    /// <summary>
    /// Compares the end of the first interval with the start of the second interval.
    /// </summary>
    /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
    public static int CompareEndToStart<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (left.EndBound.IsUnbounded() || right.StartBound.IsUnbounded())
        {
            return 1;
        }

        int comparison = left.End.CompareTo(right.Start);
        if (comparison == 0 && (left.EndBound.IsOpen() || right.StartBound.IsOpen()))
        {
            return -1;
        }
        return comparison;
    }
}
