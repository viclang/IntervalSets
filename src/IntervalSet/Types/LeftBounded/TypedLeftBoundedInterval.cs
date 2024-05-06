using Intervals.Bounds;

namespace Intervals.Types;

public abstract record TypedLeftBoundedInterval<T, L>(T Start) : ILeftBoundedInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
{
    public Bound StartBound => L.Bound;

    public virtual bool IsEmpty => false;

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is ILeftBoundedInterval<T> otherLeftBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherLeftBounded.Start, otherLeftBounded.Start)
            && otherLeftBounded.StartBound == otherLeftBounded.StartBound;
        }
        return false;
    }

    public override string ToString() => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, Infinity)";
}
