namespace IntervalRecords.Types;
public interface IOverlaps<T>
{
    bool Overlaps(T other);
}
