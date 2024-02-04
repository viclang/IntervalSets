using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace IntervalRecords.Endpoints;
public record struct Endpoint<T>(T? Point, bool Inclusive, BoundaryType BoundaryType)
    : IComparable<Endpoint<T>>
    where T : struct, IComparable<T>
{
    public static readonly Endpoint<T> NegativeInfinity = new Endpoint<T>(null, false, BoundaryType.Lower);

    public static readonly Endpoint<T> PositiveInfinity = new Endpoint<T>(null, false, BoundaryType.Upper);


    [MemberNotNullWhen(false)]
    public readonly bool IsInfinity => Point is null;

    public readonly bool IsNegativeInfinity => Point is null && BoundaryType is BoundaryType.Lower;

    public readonly bool IsPositiveInfinity => Point is null && BoundaryType is BoundaryType.Upper;

    public readonly EndpointState State
    {
        get
        {
            if (Point is not null)
            {
                return EndpointState.Finite;
            }
            return BoundaryType is BoundaryType.Lower ? EndpointState.NegativeInfinity : EndpointState.PositiveInfinity;
        }
    }

    public int CompareTo(Endpoint<T> other)
    {
        if (IsInfinity && other.IsInfinity)
        {
            return BoundaryType.CompareTo(other.BoundaryType);
        }
        if (IsInfinity)
        {
            return BoundaryType is BoundaryType.Lower ? -1 : 1;
        }
        if (other.IsInfinity)
        {
            return other.BoundaryType is BoundaryType.Lower ? -1 : 1;
        }

        // Compare values if neither are infinity.
        var valueCompared = Point!.Value.CompareTo(other.Point!.Value);
        if (valueCompared is not 0)
        {
            return valueCompared;
        }
        // Left is greater than right when values are equal and at least one is exclusive.
        var locationCompared = BoundaryType.CompareTo(other.BoundaryType);
        if (locationCompared is not 0 && (!Inclusive || !other.Inclusive))
        {
            return -locationCompared;
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
            case BoundaryType.Lower:
                sb.Append(Inclusive ? '[' : '(');
                sb.Append(IsInfinity ? "NegativeInfinity" : Point.ToString());
                break;
            case BoundaryType.Upper:
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
        return value with { BoundaryType = BoundaryType.Upper };
    }

    public static Endpoint<T> operator -(Endpoint<T> value)
    {
        return value with { BoundaryType = BoundaryType.Lower };
    }
}
