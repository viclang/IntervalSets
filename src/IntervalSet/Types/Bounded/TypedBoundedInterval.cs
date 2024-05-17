using IntervalSet.Types.Bounded;
using System.Diagnostics.CodeAnalysis;

namespace IntervalSet.Types;

public abstract record TypedBoundedInterval<T, L, R>(T Start, T End) : IBoundedInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where L : struct, IBound
    where R : struct, IBound
{
    public Bound StartBound => L.Bound;

    public Bound EndBound => R.Bound;

    public virtual bool IsEmpty => End.CompareTo(Start) is int comparison
        && comparison < 0 || comparison == 0 && StartBound.IsOpen() && EndBound.IsOpen();

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is IBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public virtual bool Equals(TypedBoundedInterval<T, L, R>? other)
    {
        if (other is IBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End, StartBound, EndBound);
    }

    public override string ToString()
        => $"{(StartBound.IsClosed() ? '[' : '(')}{Start}, {End}{(EndBound.IsClosed() ? ']' : ')')}";
}