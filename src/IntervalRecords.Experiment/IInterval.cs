namespace IntervalRecords.Experiment;
public interface IInterval<T>
    where T : struct, IComparable<T>, ISpanParsable<T>
{
    T? Start { get; init; }
    T? End { get; init; }
}
