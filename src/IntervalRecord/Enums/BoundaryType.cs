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

    public static class BoundaryTypeExtensions
    {
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
