using System.Diagnostics.Contracts;

namespace IntervalRecords
{
    public static partial class Interval
    {
        /// <summary>
        /// Returns the maximum interval between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <returns>The interval that is greater than or equal to the other interval.</returns>
        [Pure]
        public static Interval<T> Max<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first >= second ? first : second;

        /// <summary>
        /// Returns the interval that is greater than or equal to the other interval, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
        /// <returns>The interval that is greater than or equal to the other interval based on the comparison of the selected values.</returns>
        [Pure]
        public static Interval<T> MaxBy<T, TProperty>(Interval<T> first, Interval<T> second, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(first).CompareTo(selector(second)) >= 0 ? first : second;

        /// <summary>
        /// Returns the minimum interval between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <returns>The interval that is less than or equal to the other interval.</returns>
        [Pure]
        public static Interval<T> Min<T>(Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => first <= second ? first : second;

        /// <summary>
        /// Returns the minimum interval between two intervals, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="T">The type of the interval bounds.</typeparam>
        /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
        /// <returns>The interval that is less than or equal to the other interval based on the comparison of the selected values.</returns>
        [Pure]
        public static Interval<T> MinBy<T, TProperty>(Interval<T> first, Interval<T> second, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(first).CompareTo(selector(second)) <= 0 ? first : second;
    }
}
