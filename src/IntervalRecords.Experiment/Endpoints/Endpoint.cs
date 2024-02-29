using System.Numerics;
using System.Text;

namespace IntervalRecords.Experiment.Endpoints;
public readonly record struct Endpoint<T>(T Value, BoundType BoundaryType, Direction Direction)
    : IComparable<Endpoint<T>>,
      IComparable<T>
    where T : struct, IComparable<T>
{
    public bool HasValue => BoundaryType != BoundType.Unbounded;

    public bool Inclusive => BoundaryType == BoundType.Closed;

    public static Endpoint<T> Start(T? value, bool inclusive)
        => new(value.GetValueOrDefault(), GetBoundaryType(value.HasValue, inclusive), Direction.Left);

    public static Endpoint<T> End(T? value, bool inclusive)
        => new(value.GetValueOrDefault(), GetBoundaryType(value.HasValue, inclusive), Direction.Right);

    public static BoundType GetBoundaryType(bool hasValue, bool inclusive)
    {
        if (!hasValue)
        {
            return BoundType.Unbounded;
        }
        return inclusive ? BoundType.Closed : BoundType.Open;
    }

    public int CompareTo(Endpoint<T> other)
        => CompareTo(other, BoundaryType == BoundType.Open || other.BoundaryType == BoundType.Open);

    public int ConnectedCompareTo(Endpoint<T> other)
        => CompareTo(other, (BoundaryType & other.BoundaryType) == BoundType.Open);

    private int CompareTo(Endpoint<T> other, bool exclusiveCondition)
    {
        var directionCompared = Direction.CompareTo(other.Direction);
        if (BoundaryType == BoundType.Unbounded || other.BoundaryType == BoundType.Unbounded)
        {
            return directionCompared;
        }
        var valueCompared = Value.CompareTo(other.Value);
        if (valueCompared != 0)
        {
            return valueCompared;
        }
        if (directionCompared != 0 && exclusiveCondition)
        {
            return -directionCompared;
        }
        return valueCompared;
    }

    public int CompareTo(T other)
    {
        var directionValue = ToSign((int)Direction);
        if (BoundaryType == BoundType.Unbounded)
        {
            return directionValue;
        }
        var valueCompared = Value.CompareTo(other);
        if (valueCompared == 0 && BoundaryType == BoundType.Open)
        {
            return -directionValue;
        }
        return valueCompared;
    }

    /// <summary>
    /// Converts 0 to -1 and 1 to 1
    /// </summary>
    /// <param name="bit"></param>
    /// <returns></returns>
    public static int ToSign(int bit) => (bit << 1) - 1;

    public override string? ToString() => (BoundaryType, Direction) switch
    {
        (BoundType.Unbounded, Direction.Left) => "(-Infinity",
        (BoundType.Unbounded, Direction.Right) => "Infinity)",
        (BoundType.Open, Direction.Left) => $"({Value}",
        (BoundType.Closed, Direction.Left) => $"[{Value}",
        (BoundType.Open, Direction.Right) => $"{Value})",
        (BoundType.Closed, Direction.Right) => $"{Value}]",
        _ => throw new NotImplementedException(),
    };

}