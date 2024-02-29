using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecords.Experiment;
public static partial class IntervalExtensions
{
    /// <summary>
    /// Computes the union of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Union<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IComparable<T>, ISpanParsable<T>
    {
        if (!left.IsConnected(right))
        {
            return null;
        }
        return left.Hull(right);
    }
}
