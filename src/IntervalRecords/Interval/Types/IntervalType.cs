namespace IntervalRecords
{
    /// <summary>
    /// Specifies the type of interval, whether it is closed, open-closed, closed-open, or open.
    /// </summary>
    public enum IntervalType : byte
    {
        Closed = 0,
        ClosedOpen = 1,
        OpenClosed = 2,
        Open = 3,
    }
}
