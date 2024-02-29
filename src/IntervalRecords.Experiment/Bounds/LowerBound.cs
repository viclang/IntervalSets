using IntervalRecords.Experiment.Endpoints;
using System.Numerics;

namespace IntervalRecords.Experiment.Bounds;
public record struct LowerBound<T>(T Value, BoundType BoundaryType)
    : IEquatable<UpperBound<T>>,
      IEquatable<T>,
      IComparisonOperators<LowerBound<T>, LowerBound<T>, bool>,
      IComparisonOperators<LowerBound<T>, UpperBound<T>, bool>,
      IComparisonOperators<LowerBound<T>, T, bool>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    public readonly bool Equals(UpperBound<T> other)
        => Value.Equals(other) && BoundaryType == BoundType.Closed && other.BoundaryType == BoundType.Closed;

    public readonly bool Equals(T other) => Value.Equals(other) && BoundaryType == BoundType.Closed;

    public static bool operator ==(LowerBound<T> left, T right)
    {
        return left.Equals(right);
    }

    public static bool operator ==(LowerBound<T> left, UpperBound<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(LowerBound<T> left, T right)
    {
        return !left.Equals(right);
    }

    public static bool operator !=(LowerBound<T> left, UpperBound<T> right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(LowerBound<T> left, T right)
        => left.BoundaryType == BoundType.Unbounded
        || left.Value.CompareTo(right) < 0
        || left.Value.Equals(right) && left.BoundaryType == BoundType.Open;

    public static bool operator <(LowerBound<T> left, LowerBound<T> right)
    {
        return right.BoundaryType != BoundType.Unbounded
            && (left.BoundaryType == BoundType.Unbounded
                || left.Value.CompareTo(right.Value) < 0
                || left.Value.Equals(right.Value) && left.BoundaryType.CompareTo(right.BoundaryType) < 0);
    }


    public static bool operator <(LowerBound<T> left, UpperBound<T> right)
    {
        return left.BoundaryType == BoundType.Unbounded || right.BoundaryType == BoundType.Unbounded
            || left.Value.CompareTo(right.Value) < 0
            || left.Value.Equals(right.Value) && left.BoundaryType.CompareTo(right.BoundaryType) < 0;
    }

    public static bool operator >(LowerBound<T> left, LowerBound<T> right)
    {
        return left.BoundaryType != BoundType.Unbounded
            && (right.BoundaryType == BoundType.Unbounded
            || left.Value.CompareTo(right.Value) > 0
            || left.Value.Equals(right.Value)
                && left.BoundaryType.CompareTo(right.BoundaryType) > 0);
    }

    public static bool operator >(LowerBound<T> left, T right)
    => left.BoundaryType != BoundType.Unbounded && left.Value.CompareTo(right) > 0;

    public static bool operator >(LowerBound<T> left, UpperBound<T> right)
    {
        return right.BoundaryType != BoundType.Unbounded && left.BoundaryType != BoundType.Unbounded
            && (left.Value.CompareTo(right.Value) > 0
            || left.Value.Equals(right.Value)
                && left.BoundaryType.CompareTo(right.BoundaryType) > 0);
    }

    public static bool operator <=(LowerBound<T> left, LowerBound<T> right)
    {
        return left < right || left == right;
    }

    public static bool operator <=(LowerBound<T> left, T right)
    {
        return left < right || left == right;
    }

    public static bool operator <=(LowerBound<T> left, UpperBound<T> right)
    {
        return left < right || left == right;
    }

    public static bool operator >=(LowerBound<T> left, LowerBound<T> right)
    {
        return left > right || left == right;
    }

    public static bool operator >=(LowerBound<T> left, T right)
    {
        return left > right || left == right;
    }

    public static bool operator >=(LowerBound<T> left, UpperBound<T> right)
    {
        return left > right || left == right;
    }
}
