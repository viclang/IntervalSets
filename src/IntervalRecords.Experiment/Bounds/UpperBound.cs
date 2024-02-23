namespace IntervalRecords.Experiment.Bounds;
public readonly record struct UpperBound<T>(T? Point, bool Inclusive) : IComparable<UpperBound<T>>, IComparable<LowerBound<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public int CompareTo(UpperBound<T> other) => (Point.HasValue, other.Point.HasValue) switch
    {
        (false, false) => 0,
        (false, true) => 1,
        (true, false) => -1,
        (true, true) => Point!.Value.Equals(other.Point!.Value)
            ? Inclusive.CompareTo(other.Inclusive)
            : Point.Value.CompareTo(other.Point.Value),
    };

    public int CompareTo(LowerBound<T> other)
    {
        if (Point is null || other.Point is null)
        {
            return 1;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && (!Inclusive || !other.Inclusive))
        {
            return -1;
        }
        return endStartComparison;
    }
}
