namespace Intervals.Types;
public interface IAbstractInterval<T> : IEquatable<IAbstractInterval<T>>
{
    public bool IsEmpty { get; }
}
