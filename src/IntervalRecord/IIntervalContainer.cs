namespace IntervalRecord
{
    internal interface IIntervalContainer<T>
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        Interval<T> Interval { get; }
    }
}
