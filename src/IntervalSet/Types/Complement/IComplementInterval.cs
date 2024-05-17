namespace IntervalSet.Types;
public interface IComplementInterval<T> : IAbstractInterval<T>
    where T : notnull, IComparable<T>, ISpanParsable<T>
{
    T Start { get; }

    T End { get; }
}
