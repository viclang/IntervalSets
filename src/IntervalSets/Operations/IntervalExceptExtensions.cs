using IntervalSets.Types;

namespace IntervalSets.Operations;
public static class IntervalExceptExtensions
{
    /// <summary>
    /// Produces the difference between two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public static IEnumerable<Interval<T>> Except<T>(this Interval<T> left, Interval<T> right)
    where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (left == right)
        {
            yield break;
        }
        if (!left.Overlaps(right))
        {
            yield return left;
            yield return right;
            yield break;
        }
        var compareStart = left.CompareStart(right);
        if (compareStart == -1)
        {
            yield return new(left.Start, right.Start, left.StartBound, Invert(right.StartBound));
        }
        else if (compareStart == 1)
        {
            yield return new(right.Start, left.Start, right.StartBound, Invert(left.StartBound));
        }
        var compareEnd = left.CompareEnd(right);
        if (compareEnd == -1)
        {
            yield return new(left.End, right.End, Invert(left.EndBound), right.EndBound);
        }
        else if (compareEnd == 1)
        {
            yield return new(right.End, left.End, Invert(right.EndBound), left.EndBound);
        }
    }

    private static Bound Invert(this Bound bound)
    {
        return bound switch
        {
            Bound.Open => Bound.Closed,
            Bound.Closed => Bound.Open,
            _ => bound
        };
    }
}
