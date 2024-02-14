using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace IntervalRecords.Experiment.Endpoints;
public record struct Endpoint<T>(T? Point, bool Inclusive = true, EndpointType EndpointType = EndpointType.Upper)
    : IComparable<Endpoint<T>>, IParsable<Endpoint<T>>, ISpanParsable<Endpoint<T>>
    where T : struct, IComparable<T>, ISpanParsable<T>
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

    public readonly EndpointState State => Point is not null
        ? EndpointState.Finite
        : EndpointType is EndpointType.Lower ? EndpointState.NegativeInfinity : EndpointState.PositiveInfinity;

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
        var valueCompared = Point.Value.CompareTo(other.Point!.Value);
        if (valueCompared is 0 && (Inclusive && other.Inclusive))
        {
            return -(EndpointType.CompareTo(other.EndpointType));
        }
        return valueCompared;
    }

    public override readonly int GetHashCode() => HashCode.Combine(Point, Inclusive, EndpointType);

    public override readonly string? ToString()
    {
        var sb = new StringBuilder();
        switch (EndpointType)
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

    public static Endpoint<T> Parse(string s, IFormatProvider? provider)
    {
        return Parse(s.AsSpan(), provider);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Endpoint<T> result)
    {
        return TryParse(s.AsSpan(), provider, out result);
    }

    public static Endpoint<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (!IsValidEndpoint(s))
        {
            throw new ArgumentException(s.ToString(), nameof(s));
        }
        (bool inclusive, EndpointType type) endpoint;
        if (IsLowerEndpoint(s))
        {
            endpoint = ParseEndpointType(s[0]);
            return new Endpoint<T>(ParsePoint(s[1..], provider), endpoint.inclusive, endpoint.type);
        }
        endpoint = ParseEndpointType(s[^1]);
        return new Endpoint<T>(ParsePoint(s[..^1], provider), endpoint.inclusive, endpoint.type);
    }

    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out Endpoint<T> result)
    {
        result = default;
        if (!IsValidEndpoint(s))
        {
            return false;
        }
        (bool inclusive, EndpointType type) endpoint;
        T? point;
        if (IsLowerEndpoint(s))
        {
            if (TryParsePoint(s[1..], provider, out point))
            {
                endpoint = ParseEndpointType(s[0]);
                result = new Endpoint<T>(point, endpoint.inclusive, endpoint.type);
                return true;
            }
        }        
        else if(TryParsePoint(s[..^1], provider, out point))
        {
            endpoint = ParseEndpointType(s[^1]);
            result = new Endpoint<T>(point, endpoint.inclusive, endpoint.type);
            return true;
        }
        return false;
    }

    private static T? ParsePoint(ReadOnlySpan<char> value, IFormatProvider? provider)
    {
        if (RepresentsInfinity(value))
        {
            return null;
        }
        return T.Parse(value, provider);
    }

    private static bool TryParsePoint(ReadOnlySpan<char> value, IFormatProvider? provider, out T? result)
    {
        if (RepresentsInfinity(value))
        {
            result = null;
            return true;
        }
        if (T.TryParse(value, provider, out var point))
        {
            result = point;
            return true;
        }
        result = default;
        return false;
    }
    private static bool IsValidEndpoint(ReadOnlySpan<char> s)
    {
        return s.Length > 1 && ("[(".Contains(s[0]) || "])".Contains(s[^1]));
    }

    private static bool IsLowerEndpoint(ReadOnlySpan<char> s)
    {
        return s[0] == '[' || s[0] == '(';
    }

    private static bool RepresentsInfinity(ReadOnlySpan<char> value)
    {
        return value.Contains("infinity", StringComparison.OrdinalIgnoreCase) || value.Contains('∞');
    }

    private static (bool inclusive, EndpointType type) ParseEndpointType(char type) => type switch
    {
        '[' => (true, EndpointType.Lower),
        '(' => (false, EndpointType.Lower),
        ']' => (true, EndpointType.Upper),
        ')' => (false, EndpointType.Upper),
        _ => throw new ArgumentException(type.ToString(), nameof(type))
    };

    public static implicit operator Endpoint<T>(T? value) => new(value);
    public static explicit operator T?(Endpoint<T> value) => value.Point;

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
        return value with { EndpointType = EndpointType.Upper };
    }

    public static Endpoint<T> operator -(Endpoint<T> value)
    {
        return value with { EndpointType = EndpointType.Lower };
    }
}
