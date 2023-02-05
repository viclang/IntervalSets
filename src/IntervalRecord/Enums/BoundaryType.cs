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
}
