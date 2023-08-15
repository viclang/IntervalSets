namespace IntervalRecords.Extensions
{
    /// <summary>
    /// Specifies the bounded state of an interval.
    /// </summary>
    public enum BoundaryState : byte
    {
        /// <summary>
        /// Both endpoints are bounded.
        /// </summary>
        Bounded = 0,
        /// <summary>
        /// Both endpoints are unbounded.
        /// </summary>
        Unbounded = 1,
        /// <summary>
        /// The left endpoint is bounded, the right endpoint is unbounded.
        /// </summary>
        LeftBounded = 2,
        /// <summary>
        /// The right endpoint is bounded, the left endpoint is unbounded.
        /// </summary>
        RightBounded = 3,
    }
}
