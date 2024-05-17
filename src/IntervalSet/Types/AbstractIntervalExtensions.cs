using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntervalSet.Types.Unbounded;

namespace IntervalSet.Types;
public static class AbstractIntervalExtensions
{
    public static TResult Match<T, TResult>(
        this IAbstractInterval<T> abstractInterval,
        Func<IBoundedInterval<T>, TResult> bounded,
        Func<ILeftBoundedInterval<T>, TResult> leftBounded,
        Func<IRightBoundedInterval<T>, TResult> rightBounded,
        Func<IComplementInterval<T>, TResult> complement)
        where T : notnull, IComparable<T>, ISpanParsable<T>
        => abstractInterval switch
        {
            IBoundedInterval<T> boundedInterval => bounded(boundedInterval),
            ILeftBoundedInterval<T> leftBoundedInterval => leftBounded(leftBoundedInterval),
            IRightBoundedInterval<T> rightBoundedInterval => rightBounded(rightBoundedInterval),
            IComplementInterval<T> complementInterval => complement(complementInterval),
            _ => throw new InvalidOperationException()
        };
}
