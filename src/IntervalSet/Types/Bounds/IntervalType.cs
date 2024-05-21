namespace IntervalSet.Types;

[Flags]
public enum IntervalType : byte
{
    Open = 0b00_00,
    OpenClosed = 0b00_01,
    OpenUnbounded = 0b00_10,
    ClosedOpen = 0b01_00,
    Closed = 0b01_01,
    ClosedUnbounded = 0b01_10,
    UnboundedOpen = 0b10_00,
    UnboundedClosed = 0b10_01,
    Unbounded = 0b10_10
}