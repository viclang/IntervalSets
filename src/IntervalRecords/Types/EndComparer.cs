namespace IntervalRecords.Types;
public class EndComparer<T> : Comparer<Interval<T>>
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
{
    public override int Compare(Interval<T>? first, Interval<T>? second)
    {
        if (first is null)
        {
            throw new ArgumentNullException(nameof(first));
        }

        if (second is null)
        {
            throw new ArgumentNullException(nameof(second));
        }
        var result = first.End.CompareTo(second.End);
        if(result == 0)
        {
            return first.EndInclusive.CompareTo(second.EndInclusive);
        }
        return result;
    }
}
