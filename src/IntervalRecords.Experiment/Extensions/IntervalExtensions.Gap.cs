using IntervalRecords.Experiment.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment.Extensions;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T>? Gap<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsDisjoint(right))
        {
            return null;
        }
        if (left.CompareStartToEnd(right) == 1)
        {
            return new(right.End, left.Start, !right.EndInclusive, !left.StartInclusive);
        }
        if (left.CompareEndToStart(right) == -1)
        {
            return new(left.End, right.Start, !left.EndInclusive, !right.StartInclusive);
        }
        return null;
    }
}
