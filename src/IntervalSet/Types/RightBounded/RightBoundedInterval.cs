using IntervalSet.Bounds;

namespace IntervalSet.Types;
public record RightBoundedInterval<T>(T End, Bound EndBound) : IRightBoundedInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    public bool IsEmpty => false;

    public bool Equals(IAbstractInterval<T>? other)
    {
        if(other is IRightBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public override string ToString() => $"(-Infinity, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}

public record RightBoundedInterval<T, R>(T End) : TypedRightBoundedInterval<T, R>(End)
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where R : struct, IBound
{
    public override bool IsEmpty => false;

    public static implicit operator RightBoundedInterval<T>(RightBoundedInterval<T, R> rigthBoundedInterval)
        => new(rigthBoundedInterval.End, R.Bound);

    public override string ToString() => $"(-Infinity, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}
