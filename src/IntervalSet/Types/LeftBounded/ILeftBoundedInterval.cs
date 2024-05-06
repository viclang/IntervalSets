using Intervals.Bounds;

namespace Intervals.Types;
public interface ILeftBoundedInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    T Start { get; }

    Bound StartBound { get; }
}
