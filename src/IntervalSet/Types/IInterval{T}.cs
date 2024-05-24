namespace IntervalSet.Types;
public interface IInterval<T>
{
    public abstract T Start { get; }

    public abstract T End { get; }

    public abstract Bound StartBound { get; }

    public abstract Bound EndBound { get; }

    public abstract bool IsEmpty { get; }
}
