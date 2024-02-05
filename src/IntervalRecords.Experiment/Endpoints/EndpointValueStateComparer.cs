using System.Diagnostics.CodeAnalysis;

namespace IntervalRecords.Experiment.Endpoints;
public class EndpointValueStateComparer<T> : IComparer<Endpoint<T>>, IEqualityComparer<Endpoint<T>>
    where T : struct, IComparable<T>
{

    public int Compare(Endpoint<T> x, Endpoint<T> y)
    {
        return x.Point.HasValue && y.Point.HasValue ? x.Point.Value.CompareTo(y.Point.Value) : x.State.CompareTo(y.State);
    }

    public bool Equals(Endpoint<T> x, Endpoint<T> y)
    {
        return x.Point.HasValue ? x.Point.Value.Equals(y.Point) : x.State.Equals(y.State);
    }

    public int GetHashCode([DisallowNull] Endpoint<T> obj)
    {
        return HashCode.Combine(obj.Point, obj.State);
    }
}
