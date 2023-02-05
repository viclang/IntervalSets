namespace IntervalRecord
{
    public static partial class Interval
    {
        private readonly static Dictionary<ValueTuple<bool, bool>, IntervalType> TypeLookup = new()
        {
            { (true, true) , IntervalType.Closed },
            { (true, false), IntervalType.ClosedOpen },
            { (false, true), IntervalType.OpenClosed },
            { (false, false), IntervalType.Open },
        };

        private readonly static Dictionary<IntervalType, ValueTuple<bool, bool>> TupleLookup = 
            TypeLookup.ToDictionary(x => x.Value, x => x.Key);

        public static IntervalType GetIntervalType<T>(this Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => TypeLookup[(interval.StartInclusive, interval.EndInclusive)];

        public static (bool, bool) ToTuple(this IntervalType intervalType)
            => TupleLookup[intervalType];
    }
}
