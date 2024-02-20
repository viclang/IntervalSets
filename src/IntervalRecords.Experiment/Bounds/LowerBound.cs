namespace IntervalRecords.Experiment.Bounds;
internal record struct LowerBound<T>(T? Bound, bool Inclusive) : IComparable<LowerBound<T>>, IComparable<UpperBound<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public readonly int CompareTo(LowerBound<T> other) => (Bound.HasValue, other.Bound.HasValue) switch
    {
        (false, false) => 0,
        (false, true) => -1,
        (true, false) => 1,
        (true, true) => Bound!.Value.Equals(other.Bound!.Value)
            ? Inclusive.CompareTo(other.Inclusive)
            : Bound.Value.CompareTo(other.Bound.Value),
    };

    public readonly int CompareTo(UpperBound<T> other)
    {
        if (Bound is null || other.Bound is null)
        {
            return -1;
        }
        var endStartComparison = Bound.Value.CompareTo(other.Bound.Value);
        if (endStartComparison == 0 && (!Inclusive || !other.Inclusive))
        {
            return 1;
        }
        return endStartComparison;
    }

    public readonly int ConnectedCompareTo(UpperBound<T> other)
    {
        if (Bound is null || other.Bound is null)
        {
            return -1;
        }
        var endStartComparison = Bound.Value.CompareTo(other.Bound.Value);
        if (endStartComparison == 0 && !Inclusive && !other.Inclusive)
        {
            return 1;
        }
        return endStartComparison;
    }
}
