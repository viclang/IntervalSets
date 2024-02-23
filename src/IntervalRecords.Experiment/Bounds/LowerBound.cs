namespace IntervalRecords.Experiment.Bounds;
public record struct LowerBound<T>(T? Point, bool Inclusive) : IComparable<LowerBound<T>>, IComparable<UpperBound<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public readonly int CompareTo(LowerBound<T> other) => (Point.HasValue, other.Point.HasValue) switch
    {
        (false, false) => 0,
        (false, true) => -1,
        (true, false) => 1,
        (true, true) => Point!.Value.Equals(other.Point!.Value)
            ? Inclusive.CompareTo(other.Inclusive)
            : Point.Value.CompareTo(other.Point.Value),
    };

    public readonly int CompareTo(UpperBound<T> other)
    {
        if (Point is null || other.Point is null)
        {
            return -1;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && (!Inclusive || !other.Inclusive))
        {
            return 1;
        }
        return endStartComparison;
    }

    public readonly int ConnectedCompareTo(UpperBound<T> other)
    {
        if (Point is null || other.Point is null)
        {
            return -1;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && !Inclusive && !other.Inclusive)
        {
            return 1;
        }
        return endStartComparison;
    }
}
