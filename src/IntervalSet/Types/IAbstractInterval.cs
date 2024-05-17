namespace IntervalSet.Types;
public interface IAbstractInterval<T> : IEquatable<IAbstractInterval<T>>
{
    public Bound StartBound { get; }

    public Bound EndBound { get; }

    public bool IsEmpty { get; }
}
