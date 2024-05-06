using Intervals.Bounds;
using Intervals.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intervals.Operations.Overlaps;
public static class ComplementOverlapsExtensions
{
    /// <summary>
    /// Returns a boolean value indicating if the left interval overlaps with the right interval.
    /// </summary>
    /// <returns>True if the left interval and the right interval overlap.</returns>
    public static bool Overlaps<T>(this IComplementInterval<T> left, IComplementInterval<T> right)
        where T : notnull, IComparable<T>, ISpanParsable<T>
    {
        var startComparison = left.Start.CompareTo(right.End);
        var endComparison = right.Start.CompareTo(left.End);

        return startComparison > 0 && endComparison > 0
            || startComparison == 0 && (left.StartBound.IsOpen() || right.EndBound.IsOpen())
            || endComparison == 0 && (left.EndBound.IsOpen() || right.StartBound.IsOpen());
    }
}
