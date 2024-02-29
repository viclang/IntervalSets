namespace IntervalRecords.Experiment;
public abstract record class AbstractInterval<TLeft, TRight>
    : IComparable<AbstractInterval<TLeft, TRight>>
    where TLeft : IComparable<TLeft>, IComparable<TRight>
    where TRight : IComparable<TRight>, IComparable<TLeft>
{
    internal TLeft LeftEndpoint { get; init; }
    internal TRight RightEndpoint { get; init; }

    protected AbstractInterval(TLeft left, TRight right)
    {
        LeftEndpoint = left;
        RightEndpoint = right;
    }

    /// <summary>
    /// Returns a boolean value indicating if the current interval overlaps with the other interval.
    /// </summary>
    /// <param name="other">The interval to check for overlapping with the current interval.</param>
    /// <returns>True if the current interval and the other interval overlap, False otherwise.</returns>
    public bool Overlaps(AbstractInterval<TLeft, TRight> other)
    {
        return LeftEndpoint.CompareTo(other.RightEndpoint) <= 0
            && other.LeftEndpoint.CompareTo(RightEndpoint) <= 0;
    }

    public static int Compare(AbstractInterval<TLeft, TRight> left, AbstractInterval<TLeft, TRight> right, IntervalComparison comparisonType) => comparisonType switch
    {
        IntervalComparison.Interval => left.CompareTo(right),
        IntervalComparison.Start => left.LeftEndpoint.CompareTo(right.LeftEndpoint),
        IntervalComparison.End => left.RightEndpoint.CompareTo(right.RightEndpoint),
        IntervalComparison.StartToEnd => left.LeftEndpoint.CompareTo(right.RightEndpoint),
        IntervalComparison.EndToStart => left.RightEndpoint.CompareTo(right.LeftEndpoint),
        _ => throw new NotImplementedException(),
    };


    public int CompareTo(AbstractInterval<TLeft, TRight>? other)
    {
        if (other == null || IsGreaterThan(this, other))
        {
            return 1;
        }
        if (IsLessThan(this, other))
        {
            return -1;
        }
        return 0;
    }

    protected static bool IsLessThan(AbstractInterval<TLeft, TRight> left, AbstractInterval<TLeft, TRight> right)
    {
        int compareEnd = left.RightEndpoint.CompareTo(right.RightEndpoint);
        return compareEnd == -1 || (compareEnd == 0 && left.LeftEndpoint.CompareTo(right.LeftEndpoint) == 1);
    }

    protected static bool IsGreaterThan(AbstractInterval<TLeft, TRight> left, AbstractInterval<TLeft, TRight> right)
    {
        int compareEnd = left.RightEndpoint.CompareTo(right.RightEndpoint);
        return compareEnd == 1 || (compareEnd == 0 && left.LeftEndpoint.CompareTo(right.LeftEndpoint) == -1);
    }
}
