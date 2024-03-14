namespace IntervalRecords.Experiment.Endpoints;
public readonly record struct LeftEndpoint<T, TBound>(T Value) : ILeftEndpoint<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
    where TBound : IBound, new()
{
    public readonly Bound Bound => TBound.Bound;

    public readonly bool IsClosed => Bound == Bound.Closed;

    public readonly bool IsOpen => Bound == Bound.Open;

    public readonly bool IsUnbounded => Bound == Bound.Unbounded;

    public int CompareTo(ILeftEndpoint<T>? other)
    {
        if (other == null)
        {
            return 1;
        }

        return (IsUnbounded, other.IsUnbounded) switch
        {
            (true, true) => 0,
            (true, false) => -1,
            (false, true) => 1,
            (false, false) =>
                Value.CompareTo(other.Value) switch
                {
                    0 => IsClosed.CompareTo(other.IsClosed),
                    var comparison => comparison
                }
        };
    }

    public int CompareTo(IRightEndpoint<T>? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (IsUnbounded || other.IsUnbounded)
        {
            return -1;
        }

        int comparison = Value.CompareTo(other.Value);
        if (comparison == 0 && (!IsClosed || !other.IsClosed))
        {
            return 1;
        }
        return comparison;
    }

    public int CompareTo(T other)
    {
        if (IsUnbounded)
        {
            return -1;
        }
        var comparison = Value.CompareTo(other);
        if (comparison == 0 && !IsClosed)
        {
            return 1;
        }
        return comparison;
    }

    public static implicit operator LeftEndpoint<T, TBound>(T value) => new(value);
}
