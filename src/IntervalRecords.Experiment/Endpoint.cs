namespace IntervalRecords.Experiment;
internal struct Endpoint<T> : IComparable<Endpoint<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    private const int lower = -1;
    private const int upper = 1;

    public T? Point { get; set; }

    public bool Inclusive { get; set; }

    public int EndpointType { get; set; }

    private Endpoint(T? point, bool inclusive, int endpointType)
    {
        Point = point;
        Inclusive = inclusive;
        EndpointType = endpointType;
    }

    public static Endpoint<T> Start(Interval<T> interval) => new(interval.Start, interval.StartInclusive, lower);
    
    public static Endpoint<T> End(Interval<T> interval) => new(interval.End, interval.EndInclusive, upper);

    public int CompareTo(Endpoint<T> other) => EndpointType == other.EndpointType
        ? ParallelCompare(other)
        : CrossCompare(other, !Inclusive || !other.Inclusive);

    public int ConnectedCompareTo(Endpoint<T> other) => EndpointType == other.EndpointType
        ? ParallelCompare(other)
        : CrossCompare(other, !Inclusive && !other.Inclusive);

    private int ParallelCompare(Endpoint<T> other)
        => (Point.HasValue, other.Point.HasValue) switch
        {
            (false, false) => 0,
            (false, true) => EndpointType,
            (true, false) => -EndpointType,
            (true, true) => Point!.Value.Equals(other.Point!.Value)
                ? Inclusive.CompareTo(other.Inclusive)
                : Point.Value.CompareTo(other.Point.Value),
        };

    private int CrossCompare(Endpoint<T> other, bool exclusiveCondition)
    {
        if (Point is null || other.Point is null)
        {
            return EndpointType;
        }
        var endStartComparison = Point.Value.CompareTo(other.Point.Value);
        if (endStartComparison == 0 && exclusiveCondition)
        {
            return other.EndpointType;
        }
        return endStartComparison;
    }
}
