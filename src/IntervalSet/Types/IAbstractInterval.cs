namespace IntervalSet.Types;
public interface IAbstractInterval<T>
{
    Bound StartBound { get; }

    Bound EndBound { get; }

    bool IsEmpty { get; }
}
