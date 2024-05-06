namespace Intervals.Bounds;

[Flags]
public enum BoundPair : byte
{
    Open = 0,
    OpenClosed = 1,
    ClosedOpen = 2,
    Closed = 3
}