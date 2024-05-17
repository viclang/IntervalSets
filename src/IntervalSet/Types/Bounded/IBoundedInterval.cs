namespace IntervalSet.Types;
public interface IBoundedInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    T Start { get; }

    T End { get; }
}
