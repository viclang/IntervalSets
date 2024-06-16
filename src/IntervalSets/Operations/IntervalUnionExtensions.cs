using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalUnionExtensions
{
    public static Interval<T> Union<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if(left.IsDisjoint(right))
        {
            throw new ArgumentException("Cannot construct union of disjoint intervals");
        }

        (T start, Bound startBound) = (left.StartBound, right.StartBound) switch
        {
            (Bound.Unbounded, _) => (left.Start, left.StartBound),
            (_, Bound.Unbounded) => (right.Start, right.StartBound),
            _ => left.Start.CompareTo(right.Start) switch
            {
                > 0 => (right.Start, right.StartBound),
                < 0 => (left.Start, left.StartBound),
                0 => (left.Start, left.StartBound.IsClosed() || right.StartBound.IsClosed() ? Bound.Closed : left.StartBound)
            }
        };

        (T end, Bound endBound) = (left.EndBound, right.EndBound) switch
        {
            (Bound.Unbounded, _) => (left.End, left.EndBound),
            (_, Bound.Unbounded) => (right.End, right.EndBound),
            _ => left.End.CompareTo(right.End) switch
            {
                < 0 => (right.End, right.EndBound),
                > 0 => (left.End, left.EndBound),
                0 => (left.End, left.EndBound.IsClosed() || right.EndBound.IsClosed() ? Bound.Closed : left.EndBound)
            }
        };

        return new Interval<T>(start, end, startBound, endBound);
    }
}
