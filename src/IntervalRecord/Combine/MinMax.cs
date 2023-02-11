using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        /// <summary>
        /// Returns the maximum interval between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must implement IEquatable, IComparable and IComparable{T} interfaces.</typeparam>
        /// <param name="a">The first interval to compare.</param>
        /// <param name="b">The second interval to compare.</param>
        /// <returns>The interval that is greater than or equal to the other interval.</returns>
        [Pure]
        public static Interval<T> Max<T>(Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => a >= b ? a : b;

        /// <summary>
        /// Returns the interval that is greater than or equal to the other interval, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must implement IEquatable, IComparable and IComparable{T} interfaces.</typeparam>
        /// <typeparam name="TProperty">The type of the property to compare, which must implement IComparable{TProperty} interface.</typeparam>
        /// <param name="a">The first interval to compare.</param>
        /// <param name="b">The second interval to compare.</param>
        /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
        /// <returns>The interval that is greater than or equal to the other interval based on the comparison of the selected values.</returns>
        [Pure]
        public static Interval<T> MaxBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(a).CompareTo(selector(b)) >= 0 ? a : b;

        /// <summary>
        /// Returns the minimum interval between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must implement IEquatable, IComparable and IComparable{T} interfaces.</typeparam>
        /// <param name="a">The first interval to compare.</param>
        /// <param name="b">The second interval to compare.</param>
        /// <returns>The interval that is less than or equal to the other interval.</returns>
        [Pure]
        public static Interval<T> Min<T>(Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => a <= b ? a : b;

        /// <summary>
        /// Returns the interval that is less than or equal to the other interval, using a specific selector function to extract the value to compare.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints, which must implement IEquatable, IComparable and IComparable{T} interfaces.</typeparam>
        /// <typeparam name="TProperty">The type of the property to compare, which must implement IComparable{TProperty} interface.</typeparam>
        [Pure]
        public static Interval<T> MinBy<T, TProperty>(Interval<T> a, Interval<T> b, Func<Interval<T>, TProperty> selector)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            where TProperty : IComparable<TProperty>
            => selector(a).CompareTo(selector(b)) <= 0 ? a : b;
    }
}
