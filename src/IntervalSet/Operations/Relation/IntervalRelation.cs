namespace IntervalSet.Operations;

/// <summary>
/// Specifies the overlapping relation between two intervals.
/// </summary>
public enum IntervalRelation : byte
{
    /// <summary>
    /// Indicates that both intervals are empty.
    /// </summary>
    BothEmpty = 0,
    /// <summary>
    /// Indicates that the first interval is empty.
    /// </summary>
    FirstEmpty = 1,
    /// <summary>
    /// Indicates that the second interval is empty.
    /// </summary>
    SecondEmpty = 2,
    /// <summary>
    /// Indicates that the first interval occurs before the second interval.
    /// </summary>
    Before = 3,
    /// <summary>
    /// Indicates that the first interval finishes exactly when the second interval starts.
    /// </summary>
    Meets = 4,
    /// <summary>
    /// Indicates that the first interval overlaps with the second interval.
    /// </summary>
    Overlaps = 5,
    /// <summary>
    /// Indicates that the first interval starts and overlaps with the second interval.
    /// </summary>
    Starts = 6,
    /// <summary>
    /// Indicates that the first interval is contained within the second interval.
    /// </summary>
    ContainedBy = 7,
    /// <summary>
    /// Indicates that the first interval finishes the second interval.
    /// </summary>
    Finishes = 8,
    /// <summary>
    /// Indicates that the two intervals are equal.
    /// </summary>
    Equal = 9,
    /// <summary>
    /// Indicates that the second interval finishes the first interval.
    /// </summary>
    FinishedBy = 10,
    /// <summary>
    /// Indicates that the second interval is contained within the first interval.
    /// </summary>
    Contains = 11,
    /// <summary>
    /// Indicates that the second interval starts and overlaps with the first interval.
    /// </summary>
    StartedBy = 12,
    /// <summary>
    /// Indicates that the second interval overlaps with the first interval.
    /// </summary>
    OverlappedBy = 13,
    /// <summary>
    /// Indicates that the first interval starts exactly when the second interval finishes.
    /// </summary>
    MetBy = 14,
    /// <summary>
    /// Indicates that the first interval occurs after the second interval.
    /// </summary>
    After = 15
}
