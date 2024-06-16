using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IntervalSets.Types;
public record struct RightEndpoint<T>
    : IComparable<LeftEndpoint<T>>,
      IComparable<RightEndpoint<T>>,
      IComparisonOperators<RightEndpoint<T>, RightEndpoint<T>, bool>,
      IComparisonOperators<RightEndpoint<T>, LeftEndpoint<T>, bool>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public T Value { get; private init; }

    public Bound Bound { get; private init; }

    public RightEndpoint(Interval<T> interval)
    {
        Value = interval.End;
        Bound = interval.EndBound;
    }
    public int CompareTo(LeftEndpoint<T> other)
    {
        if (Bound.IsUnbounded() || other.Bound.IsUnbounded())
        {
            return 1;
        }

        int comparison = Value.CompareTo(other.Value);
        if (comparison == 0 && (Bound.IsOpen() || other.Bound.IsOpen()))
        {
            return -1;
        }
        return comparison;
    }

    public int CompareTo(RightEndpoint<T> other)
    {
        return (Bound.IsUnbounded(), other.Bound.IsUnbounded()) switch
        {
            (true, true) => 0,
            (true, false) => 1,
            (false, true) => -1,
            (false, false) =>
                Value.CompareTo(other.Value) switch
                {
                    0 => Bound.CompareTo(other.Bound),
                    var comparison => comparison
                }
        };
    }

    public static bool operator ==(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) == 0;
    public static bool operator !=(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) != 0;
    public static bool operator <(RightEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) < 0;
    public static bool operator <(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) < 0;
    public static bool operator >(RightEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) > 0;
    public static bool operator >(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) > 0;
    public static bool operator <=(RightEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) <= 0;
    public static bool operator <=(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) <= 0;
    public static bool operator >=(RightEndpoint<T> left, RightEndpoint<T> right) => left.CompareTo(right) >= 0;
    public static bool operator >=(RightEndpoint<T> left, LeftEndpoint<T> right) => left.CompareTo(right) >= 0;
}
