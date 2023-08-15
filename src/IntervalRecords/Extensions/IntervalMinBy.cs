namespace IntervalRecords;
public abstract partial record Interval<T>
{
    /// <summary>
    /// Returns the minimum interval between two intervals, using a specific selector function to extract the value to compare.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
    /// <param name="other">The second interval to compare.</param>
    /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
    /// <returns>The interval that is less than or equal to the other interval based on the comparison of the selected values.</returns>
    public Interval<T> MinBy<TProperty>(Interval<T> other, Func<Interval<T>, TProperty> selector)
        where TProperty : IComparable<TProperty>
        => selector(this).CompareTo(selector(other)) <= 0 ? this : other;

    /// <summary>
    /// Returns the minimum interval between two intervals.
    /// </summary>
    /// <param name="other">The second interval to compare.</param>
    /// <returns>The interval that is less than or equal to the other interval.</returns>
    public static Interval<T> Min(Interval<T> first, Interval<T> other)
    {
        return first <= other ? first : other;
    }
}
