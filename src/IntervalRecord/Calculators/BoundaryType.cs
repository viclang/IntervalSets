using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public enum BoundaryType
    {
        Closed = 0,
        ClosedOpen = 1,
        OpenClosed = 2,
        Open = 3,
    }

    public static partial class Interval
    {
        [Pure]
        public static BoundaryType GetBoundaryType<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (value.StartInclusive, value.EndInclusive) switch
            {
                (true, true) => BoundaryType.Closed,
                (true, false) => BoundaryType.ClosedOpen,
                (false, true) => BoundaryType.OpenClosed,
                (false, false) => BoundaryType.Open,
            };

        [Pure]
        public static bool IsHalfOpen<T>(this Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => value.StartInclusive && !value.EndInclusive || !value.StartInclusive && value.EndInclusive;

        [Pure]
        public static (bool, bool) ToTuple(this BoundaryType boundaryType) => boundaryType switch
        {
            BoundaryType.Closed => (true, true),
            BoundaryType.ClosedOpen => (true, false),
            BoundaryType.OpenClosed => (false, true),
            BoundaryType.Open => (false, false),
            _ => throw new NotImplementedException()
        };
    }
}
