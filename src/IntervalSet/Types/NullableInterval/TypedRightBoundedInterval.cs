namespace IntervalSet.Types;
public abstract record TypedRightBoundedInterval<T, R>(T End) : IRightBoundedInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
    where R : struct, IBound
{
    public Bound EndBound => R.Bound;

    public virtual bool IsEmpty => false;

    public Bound StartBound => Bound.Open;

    public bool Equals(IAbstractInterval<T>? other)
    {
        if (other is IRightBoundedInterval<T> otherRightBounded)
        {
            return EqualityComparer<T>.Default.Equals(otherRightBounded.End, otherRightBounded.End)
            && otherRightBounded.EndBound == otherRightBounded.EndBound;
        }
        return false;
    }
}
