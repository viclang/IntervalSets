namespace IntervalRecords.Experiment.Bounds;
public record struct Endpoint<T>(T? Point, bool Inclusive, int Direction) : IComparable<Endpoint<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public readonly int CompareTo(Endpoint<T> other)
    {
        if (Direction == other.Direction)
        {
            return (Point.HasValue, other.Point.HasValue) switch
            {
                (false, false) => 0,
                (false, true) => Direction,
                (true, false) => -Direction,
                (true, true) => Point!.Value.Equals(other.Point!.Value)
                    ? Inclusive.CompareTo(other.Inclusive)
                    : Point.Value.CompareTo(other.Point.Value),
            };
        }
        if (Point is null || other.Point is null)
        {
            return Direction;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && (!Inclusive || !other.Inclusive))
        {
            return other.Direction;
        }
        return endStartComparison;

    }

    public readonly int ConnectedCompareTo(Endpoint<T> other)
    {
        if (Direction == other.Direction)
        {
            return (Point.HasValue, other.Point.HasValue) switch
            {
                (false, false) => 0,
                (false, true) => Direction,
                (true, false) => -Direction,
                (true, true) => Point!.Value.Equals(other.Point!.Value)
                    ? Inclusive.CompareTo(other.Inclusive)
                    : Point.Value.CompareTo(other.Point.Value),
            };
        }
        if (Point is null || other.Point is null)
        {
            return Direction;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && !Inclusive && !other.Inclusive)
        {
            return other.Direction;
        }
        return endStartComparison;
    }
}
