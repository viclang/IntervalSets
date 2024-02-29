using IntervalRecords.Experiment.Endpoints;

namespace IntervalRecords.Experiment.Bounds;
public readonly record struct UpperBound<T>(T Value, BoundType BoundaryType)
    : IComparable<UpperBound<T>>,
      IComparable<LowerBound<T>>,
      IComparable<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public int CompareTo(UpperBound<T> other)
    {
        if (BoundaryType == BoundType.Unbounded || other.BoundaryType == BoundType.Unbounded)
        {
            return BoundaryType.CompareTo(other.BoundaryType);
        }
        var valueCompared = Value.CompareTo(other.Value);
        if (valueCompared != 0)
        {
            return valueCompared;
        }
        return valueCompared;
    }

    public int CompareTo(LowerBound<T> other)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(T other)
    {
        throw new NotImplementedException();
    }
}
