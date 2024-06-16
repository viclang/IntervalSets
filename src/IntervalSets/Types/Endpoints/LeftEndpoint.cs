using System.Numerics;

namespace IntervalSets.Types;
public record struct LeftEndpoint<T>
    : IComparable<LeftEndpoint<T>>,
      IComparable<RightEndpoint<T>>,
      IComparisonOperators<LeftEndpoint<T>, LeftEndpoint<T>, bool>,
      IComparisonOperators<LeftEndpoint<T>, RightEndpoint<T>, bool>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Value { get; private init; }

    public Bound Bound { get; private init; }

    public LeftEndpoint(Interval<T> interval)
    {
        Value = interval.Start;
        Bound = interval.StartBound; 
    }

    public int CompareTo(LeftEndpoint<T> other)
    {
        return (Bound.IsUnbounded(), other.Bound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => -1,
            (false, true) => 1,
            (false, false) =>
                Value.CompareTo(other.Value) switch
                {
                    0 => Bound.CompareTo(other.Bound),
                    var comparison => comparison
                }
        };
    }

    public int CompareTo(RightEndpoint<T> other)
    {
        if (Bound.IsUnbounded() || other.Bound.IsUnbounded())
        {
            return -1;
        }

        int comparison = Value.CompareTo(other.Value);
        if (comparison == 0 && (Bound.IsOpen() || other.Bound.IsOpen()))
        {
            return 1;
        }
        return comparison;
    }

    public static bool operator ==(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) == 0;
    public static bool operator !=(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) != 0;
    public static bool operator <(LeftEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) < 0;
    public static bool operator <(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) < 0;
    public static bool operator >(LeftEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) > 0;
    public static bool operator >(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) > 0;
    public static bool operator <=(LeftEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) <= 0;
    public static bool operator <=(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) <= 0;
    public static bool operator >=(LeftEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) >= 0;
    public static bool operator >=(LeftEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) >= 0;
}
