namespace IntervalRecords.Types;
public class StartComparer<T> : Comparer<Interval<T>>
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
        var result = first.Start.CompareTo(second.Start);
        if(result == 0)
        {
            return first.StartInclusive.CompareTo(second.StartInclusive);
        }
        return result;
    }
}
