using IntervalRecord;

namespace IntervalRecord
{
    public static partial class Interval
    {
        private readonly static Dictionary<ValueTuple<bool, bool>, BoundaryType> BoundaryTypeLookup = new()
        {
            { (true, true) , BoundaryType.Closed },
            { (true, false), BoundaryType.ClosedOpen },
            { (false, true), BoundaryType.OpenClosed },
            { (false, false), BoundaryType.Open },
        };

        private readonly static Dictionary<BoundaryType, ValueTuple<bool, bool>> TupleLookup = 
            BoundaryTypeLookup.ToDictionary(x => x.Value, x => x.Key);

        public static BoundaryType GetBoundaryType<T>(this Interval<T> interval)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (interval.StartInclusive, interval.EndInclusive) switch
            {
                (true, true) => BoundaryType.Closed,
                (true, false) => BoundaryType.ClosedOpen,
                (false, true) => BoundaryType.OpenClosed,
                (false, false) => BoundaryType.Open,
            };

        public static (bool, bool) ToTuple(this BoundaryType boundaryType)
            => boundaryType switch
            {
                BoundaryType.Closed => (true, true),
                BoundaryType.ClosedOpen => (true, false),
                BoundaryType.OpenClosed => (false, true),
                BoundaryType.Open => (false, false),
                _ => throw new NotImplementedException()
            };
    }
}
