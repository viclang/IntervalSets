namespace IntervalRecords.Experiment.Endpoints;

public readonly record struct RightEndpoint<T, TBound>(T Value) : IRightEndpoint<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
    where TBound : IBound
{
    public Bound Bound => TBound.Value;

    public readonly bool IsClosed => Bound == Bound.Closed;

    public readonly bool IsOpen => Bound == Bound.Open;

    public readonly bool IsUnbounded => Bound == Bound.Unbounded;

    public int CompareTo(ILeftEndpoint<T>? other)
    {
        if (other == null || IsUnbounded || other.IsUnbounded)
        {
            return 1;
        }

        int comparison = Value.CompareTo(other.Value);
        if (comparison == 0 && (!IsClosed || !other.IsClosed))
        {
            return -1;
        }
        return comparison;
    }

    public int CompareTo(IRightEndpoint<T>? other)
    {
        if (other == null)
        {
            return 1;
        }

        return (IsUnbounded, other.IsUnbounded) switch
        {
            (true, true) => 0,
            (true, false) => 1,
            (false, true) => -1,
            (false, false) =>
                Value.CompareTo(other.Value) switch
                {
                    0 => IsClosed.CompareTo(other.IsClosed),
                    var comparison => comparison
                }
        };
    }
}
