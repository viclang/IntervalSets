namespace IntervalSet.Types;

[Flags]
public enum IntervalType : byte
{
    Open = 0,
    OpenClosed = 1,
    ClosedOpen = 2,
    Closed = 3
}