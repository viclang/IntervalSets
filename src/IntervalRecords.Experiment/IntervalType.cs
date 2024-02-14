namespace IntervalRecords.Experiment
{
    /// <summary>
    /// Specifies the type of interval, whether it is closed, open-closed, closed-open, or open.
    /// </summary>
    public enum IntervalType : byte
    {
        Open = 0,
        ClosedOpen = 1,
        OpenClosed = 2,
        Closed = 3,
    }
}
