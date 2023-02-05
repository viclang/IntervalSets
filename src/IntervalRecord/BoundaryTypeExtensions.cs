using System.Diagnostics.Contracts;

namespace IntervalRecord
{
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
