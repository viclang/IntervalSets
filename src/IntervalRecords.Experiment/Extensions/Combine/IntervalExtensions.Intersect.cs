using IntervalRecords.Experiment.Extensions.Combine;
using IntervalRecords.Experiment.Helpers;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Calculates the overlapping part of two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The Overlapping part of two intervals, otherwise returns null.</returns>
    public static Interval<T>? Intersect<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (!left.Overlaps(right))
        {
            return null;
        }
        if (left == right)
        {
            return left;
        }

        var isLeftStart = left.CompareStart(right) > 0;
        var start = isLeftStart ? left.Start : right.Start;
        var startInclusive = isLeftStart
            ? left.StartInclusive || left.Start.HasValue && right.Contains(left.Start.Value)
            : right.StartInclusive || right.Start.HasValue && left.Contains(right.Start.Value);

        var isLeftEnd = left.CompareEnd(right) < 0;
        var end = isLeftEnd ? left.End : right.End;
        var endInclusive = isLeftEnd
            ? left.EndInclusive || left.End.HasValue && right.Contains(left.End.Value)
            : right.EndInclusive || right.End.HasValue && left.Contains(right.End.Value);

        return new(start, end, startInclusive, endInclusive);
    }


    public static IEnumerable<Interval<T>> Intersect<T>(
            this IEnumerable<Interval<T>> source)
        where T : struct, IComparable<T>, ISpanParsable<T>
            => source.Pairwise((a, b) => a.Intersect(b)).Where(i => !i.IsEmpty);
}
