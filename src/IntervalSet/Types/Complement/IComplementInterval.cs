using Intervals.Bounds;

namespace Intervals.Types;
public interface IComplementInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    T Start { get; }

    T End { get; }

    Bound StartBound { get; }

    Bound EndBound { get; }
}
