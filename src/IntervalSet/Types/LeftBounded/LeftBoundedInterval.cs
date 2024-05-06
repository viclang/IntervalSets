using Intervals.Bounds;

namespace Intervals.Types;
public record LeftBoundedInterval<T>(T Start, Bound StartBound) : ILeftBoundedInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public bool IsEmpty => false;

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

public record LeftBoundedInterval<T, L>(T Start) : TypedLeftBoundedInterval<T, L>(Start)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
{
    public override bool IsEmpty => false;

    public static implicit operator LeftBoundedInterval<T>(LeftBoundedInterval<T, L> leftBoundedInterval)
        => new(leftBoundedInterval.Start, L.Bound);

    public override string ToString() => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, Infinity)";
}
