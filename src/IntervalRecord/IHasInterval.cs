namespace IntervalRecord
{
    internal interface IHasInterval<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        Interval<T> Interval { get; }
    }
}
