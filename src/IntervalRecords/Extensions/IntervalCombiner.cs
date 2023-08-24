using System.Collections;
using System.Collections.Generic;

namespace IntervalRecords.Extensions;
public static class IntervalCombiner
{
    /// <summary>
    /// Computes the union of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The union of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Union<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!left.IsConnected(right))
        {
            return null;
        }
        return left.Hull(right);
    }

    /// <summary>
    /// Computes the smallest interval that contains both input intervals.
    /// </summary>
    /// <param name="other">The other interval to compute the hull of.</param>
    /// <returns>The smallest interval that contains both input intervals.</returns>
    public static Interval<T> Hull<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (left == right)
        {
            return left;
        }
        var (start, end, startInclusive, endInclusive) = left;
        if (right.CompareStart(left) == -1)
        {
            start = right.Start;
            startInclusive = right.StartInclusive;
        }
        if (right.CompareEnd(left) == 1)
        {
            end = right.End;
            endInclusive = right.EndInclusive;
        }
        return IntervalFactory.Create(start, end, startInclusive, endInclusive);
    }

    /// <summary>
    /// Produces the difference between two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The portion of the first interval that does not overlap with the other interval, or null if the intervals do not overlap</returns>
    public static IEnumerable<Interval<T>> Except<T>(this Interval<T> left, Interval<T> right)
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (left == right)
        {
            yield break;
        }
        var intersect = left.Intersect(right);
        if (intersect == null)
        {
            yield return left;
            yield return right;
            yield break;
        }

        if (left.CompareStart(intersect) < 0)
        {
            yield return IntervalFactory.Create(left.Start, intersect.Start, left.StartInclusive, !intersect.StartInclusive);
        }
        if (left.CompareEnd(intersect) > 0)
        {
            yield return IntervalFactory.Create(intersect.End, left.End, !intersect.EndInclusive, left.EndInclusive);
        }

        if (right.CompareStart(intersect) < 0)
        {
            yield return IntervalFactory.Create(right.Start, intersect.Start, right.StartInclusive, !intersect.StartInclusive);
        }
        if (right.CompareEnd(intersect) > 0)
        {
            yield return IntervalFactory.Create(intersect.End, right.End, !intersect.EndInclusive, right.EndInclusive);
        }
        
    }


    /// <summary>
    /// Calculates the intersect of two intervals if they overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The intersect of the two intervals if they overlap, otherwise returns null.</returns>
    public static Interval<T>? Intersect<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (!left.Overlaps(right))
        {
            return null;
        }
        if (left == right)
        {
            return left;
        }
        var (start, end, startInclusive, endInclusive) = left;
        if (left.CompareStart(right) == -1)
        {
            start = right.Start;
            startInclusive = right.StartInclusive;
        }
        if (left.CompareEnd(right) == 1)
        {
            end = right.End;
            endInclusive = right.EndInclusive;
        }
        return IntervalFactory.Create(start, end, startInclusive, endInclusive);
    }

    /// <summary>
    /// Returns the gap between two intervals, or null if the two intervals overlap.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The gap between the two intervals, if any, or null if the two intervals overlap.</returns>
    public static Interval<T>? Gap<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        if (left.IsConnected(right))
        {
            return null;
        }
        if (left.CompareStartToEnd(right) == 1)
        {
            return IntervalFactory.Create(right.End, left.Start, !right.EndInclusive, !left.StartInclusive);
        }
        if (left.CompareEndToStart(right) == -1)
        {
            return IntervalFactory.Create(left.End, right.Start, !left.EndInclusive, !right.StartInclusive);
        }
        return null;
    }

    /// <summary>
    /// Returns the minimum interval between two intervals, using a specific selector function to extract the value to compare.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
    /// <returns>The interval that is less than or equal to the other interval based on the comparison of the selected values.</returns>
    public static Interval<T> MinBy<T, TProperty>(this Interval<T> left, Interval<T> right, Func<Interval<T>, TProperty> selector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TProperty : IComparable<TProperty>
    {
        return selector(left).CompareTo(selector(right)) < 0 ? left : right;
    }

    /// <summary>
    /// Returns the minimum interval between two intervals.
    /// </summary>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <returns>The interval that is less than or equal to the other interval.</returns>
    public static Interval<T> Min<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return left <= right ? left : right;
    }

    public static TProperty Min<T, TProperty>(this Interval<T> left, Interval<T> right, Func<Interval<T>, TProperty> selector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TProperty : IComparable<TProperty>
    {
        TProperty firstValue = selector(left);
        TProperty secondValue = selector(right);
        return firstValue.CompareTo(secondValue) < 0 ? firstValue : secondValue;
    }

    /// <summary>
    /// Returns the interval that is greater than or equal to the other interval, using a specific selector function to extract the value to compare.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <typeparam name="TProperty">The type of the property to compare.</typeparam>
    /// <param name="left">The left interval.</param>
    /// <param name="right">The right interval.</param>
    /// <param name="selector">The selector function to extract the value to compare from the intervals.</param>
    /// <returns>The interval that is greater than or equal to the other interval based on the comparison of the selected values.</returns>
    public static Interval<T> MaxBy<T, TProperty>(this Interval<T> left, Interval<T> right, Func<Interval<T>, TProperty> selector)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
        where TProperty : IComparable<TProperty>
    {
        return selector(left).CompareTo(selector(right)) > 0 ? left : right;
    }

    /// <summary>
    /// Returns the maximum interval between two intervals.
    /// </summary>
    /// <typeparam name="T">The type of the interval bounds.</typeparam>
    /// <param name="left">The first interval to compare.</param>
    /// <param name="right">The second interval to compare.</param>
    /// <returns>The interval that is greater than or equal to the other interval.</returns>
    public static Interval<T> Max<T>(this Interval<T> left, Interval<T> right)
        where T : struct, IEquatable<T>, IComparable<T>, IComparable
    {
        return left >= right ? left : right;
    }

    public static TProperty Max<T, TProperty>(this Interval<T> first, Interval<T> second, Func<Interval<T>, TProperty> selector)
    where T : struct, IEquatable<T>, IComparable<T>, IComparable
    where TProperty : IComparable<TProperty>
    {
        TProperty firstValue = selector(first);
        TProperty secondValue = selector(second);
        return firstValue.CompareTo(secondValue) > 0 ? firstValue : secondValue;
    }
}
