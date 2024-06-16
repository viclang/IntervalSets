using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalIntersectExtensions
{
    public static Interval<T, L, R> Intersect<T, L, R>(this Interval<T, L, R> left, Interval<T, L, R> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        where L : struct, IBounded
        where R : struct, IBounded
    {
        if (left.IsDisjoint(right))
        {
            return Interval<T, L, R>.Empty;
        }
        T start = (left.Start.CompareTo(right.Start) > 0) ? left.Start : right.Start;
        T end = (left.End.CompareTo(right.End) < 0) ? left.End : right.End;
        return new Interval<T, L, R>(start, end);
    }

    public static Interval<T> Intersect<T>(this Interval<T> left, Interval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        if (left.IsDisjoint(right))
        {
            return Interval<T>.Empty;
        }

        (T start, Bound startBound) = (left.StartBound, right.StartBound) switch
        {
            (Bound.Unbounded, _) => (right.Start, right.StartBound),
            (_, Bound.Unbounded) => (left.Start, left.StartBound),
            _ => left.Start.CompareTo(right.Start) switch
            {
                > 0 => (left.Start, left.StartBound),
                < 0 => (right.Start, right.StartBound),
                0 => (left.Start, left.StartBound.IsClosed() || right.StartBound.IsClosed() ? Bound.Closed : left.StartBound)
            }
        };

        (T end, Bound endBound) = (left.EndBound, right.EndBound) switch
        {
            (Bound.Unbounded, _) => (right.End, right.EndBound),
            (_, Bound.Unbounded) => (left.End, left.EndBound),
            _ => left.End.CompareTo(right.End) switch
            {
                < 0 => (left.End, left.EndBound),
                > 0 => (right.End, right.EndBound),
                0 => (left.End, left.EndBound.IsClosed() || right.EndBound.IsClosed() ? Bound.Closed : left.EndBound)
            }
        };
        
        return new Interval<T>(start, end, startBound, endBound);
    }
}
