using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IntervalRecords.Experiment.Endpoints;
public record struct Endpoint<T>(T? Point, bool Inclusive, EndpointType BoundaryType)
    : IComparable<Endpoint<T>>
    where T : struct, IComparable<T>
{
    public static readonly EndpointValueStateComparer<T> PointValueStateComparer = new();

    public static readonly Endpoint<T> NegativeInfinity = new Endpoint<T>(null, false, EndpointType.Lower);

    public static readonly Endpoint<T> PositiveInfinity = new Endpoint<T>(null, false, EndpointType.Upper);

    [MemberNotNullWhen(true, "Point")]
    public readonly bool IsFinite => Point is not null;

    [MemberNotNullWhen(false, "Point")]
    public readonly bool IsInfinity => Point is null;

    public readonly bool IsNegativeInfinity => State is EndpointState.NegativeInfinity;

    public readonly bool IsPositiveInfinity => State is EndpointState.PositiveInfinity;

    public readonly EndpointState State
    {
        get
        {
            if (Point is not null) return EndpointState.Finite;
            return BoundaryType is EndpointType.Lower ? EndpointState.NegativeInfinity : EndpointState.PositiveInfinity;
        }
    }

    public readonly bool PointEquals(Endpoint<T> other)
    {
        return PointValueStateComparer.Equals(this, other);
    }

    public readonly int PointCompareTo(Endpoint<T> other)
    {
        return PointValueStateComparer.Compare(this, other);
    }

    public readonly int CompareTo(Endpoint<T> other)
    {
        if (IsInfinity || other.IsInfinity)
        {
            return State.CompareTo(other.State);
        }
        // Compare values if neither are infinity.
        var valueCompared = Point.Value.CompareTo(other.Point!.Value);
        if (valueCompared is not 0)
        {
            return valueCompared;
        }
        var boundaryTypeCompared = BoundaryType.CompareTo(other.BoundaryType);
        if (boundaryTypeCompared is not 0)
        {
            return boundaryTypeCompared;
        }
        // Inclusive is greater than Exclusive when values and locations are equal.
        return Inclusive.CompareTo(other.Inclusive);
    }

    public override readonly int GetHashCode() => HashCode.Combine(Point, Inclusive, BoundaryType);

    public override readonly string? ToString()
    {
        var sb = new StringBuilder();
        switch (BoundaryType)
        {
            case EndpointType.Lower:
                sb.Append(Inclusive ? '[' : '(');
                sb.Append(IsInfinity ? "NegativeInfinity" : Point.ToString());
                break;
            case EndpointType.Upper:
                sb.Append(IsInfinity ? "PositiveInfinity" : Point.ToString());
                sb.Append(Inclusive ? ']' : ')');
                break;
            default:
                throw new NotSupportedException();
        }
        return sb.ToString();
    }

    public static bool operator <(Endpoint<T> left, Endpoint<T> right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Endpoint<T> left, Endpoint<T> right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Endpoint<T> left, Endpoint<T> right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Endpoint<T> left, Endpoint<T> right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static Endpoint<T> operator +(Endpoint<T> value)
    {
        return value with { BoundaryType = EndpointType.Upper };
    }

    public static Endpoint<T> operator -(Endpoint<T> value)
    {
        return value with { BoundaryType = EndpointType.Lower };
    }
}
