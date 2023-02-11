namespace IntervalRecord
{
    /// <summary>
    /// Defines a container for an Interval of type T/>.
    /// </summary>
    /// <typeparam name="T">The type of values to store in the interval</typeparam>
    internal interface IIntervalContainer<T> where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        /// <summary>
        /// The interval stored in the container.
        /// </summary>
        Interval<T> Interval { get; }
    }
}
