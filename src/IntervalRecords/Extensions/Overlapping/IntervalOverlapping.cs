namespace IntervalRecords.Extensions
{
    /// <summary>
    /// Specifies the overlapping relation between two intervals.
    /// </summary>
    public enum IntervalOverlapping : byte
    {
        /// <summary>
        /// Indicates that the first interval occurs before the second interval.
        /// </summary>
        Before = 0,
        /// <summary>
        /// Indicates that the first interval finishes exactly when the second interval starts.
        /// </summary>
        Meets = 1,
        /// <summary>
        /// Indicates that the first interval overlaps with the second interval.
        /// </summary>
        Overlaps = 2,
        /// <summary>
        /// Indicates that the first interval starts and overlaps with the second interval.
        /// </summary>
        Starts = 3,
        /// <summary>
        /// Indicates that the first interval is contained within the second interval.
        /// </summary>
        ContainedBy = 4,
        /// <summary>
        /// Indicates that the first interval finishes the second interval.
        /// </summary>
        Finishes = 5,
        /// <summary>
        /// Indicates that the two intervals are equal.
        /// </summary>
        Equal = 6,
        /// <summary>
        /// Indicates that the second interval finishes the first interval.
        /// </summary>
        FinishedBy = 7,
        /// <summary>
        /// Indicates that the second interval is contained within the first interval.
        /// </summary>
        Contains = 8,
        /// <summary>
        /// Indicates that the second interval starts and overlaps with the first interval.
        /// </summary>
        StartedBy = 9,
        /// <summary>
        /// Indicates that the second interval overlaps with the first interval.
        /// </summary>
        OverlappedBy = 10,
        /// <summary>
        /// Indicates that the second interval starts exactly when the first interval finishes.
        /// </summary>
        MetBy = 11,
        /// <summary>
        /// Indicates that the first interval occurs after the second interval.
        /// </summary>
        After = 12
    }
}
