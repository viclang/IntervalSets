using IntervalRecord.Enums;

namespace IntervalRecord
{
    public static class BoundaryTypeMapper
    {
        private readonly static Dictionary<ValueTuple<bool, bool>, BoundaryType> TypeLookup = new()
        {
            { (false, false), BoundaryType.Open },
            { (true, true) , BoundaryType.Closed },
            { (false, true), BoundaryType.OpenClosed },
            { (true, false), BoundaryType.ClosedOpen }
        };

        private readonly static Dictionary<BoundaryType, ValueTuple<bool, bool>> TupleLookup = 
            TypeLookup.ToDictionary(x => x.Value, x => x.Key);

        public static BoundaryType ToType(bool startInclusive, bool endInclusive)
            => TypeLookup[(startInclusive, endInclusive)];

        public static (bool, bool) ToTuple(BoundaryType intervalType)
            => TupleLookup[intervalType];
    }
}
