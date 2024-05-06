using Intervals.Bounds;

namespace Intervals.Types;
public interface IRightBoundedInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    T End { get; }

    Bound EndBound { get; }
}
