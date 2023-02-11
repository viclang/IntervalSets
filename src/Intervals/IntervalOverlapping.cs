using System.Diagnostics.Contracts;

namespace Intervals
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

    public static partial class Interval
    {
        /// <summary>
        /// Determines interval overlapping relation between two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        [Pure]
        public static IntervalOverlapping GetIntervalOverlapping<T>(this Interval<T> first, Interval<T> second, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (first.CompareStart(second), first.CompareEnd(second)) switch
            {
                (0, 0) => IntervalOverlapping.Equal,
                (0, -1) => IntervalOverlapping.Starts,
                (1, -1) => IntervalOverlapping.ContainedBy,
                (1, 0) => IntervalOverlapping.Finishes,
                (-1, 0) => IntervalOverlapping.FinishedBy,
                (-1, 1) => IntervalOverlapping.Contains,
                (0, 1) => IntervalOverlapping.StartedBy,
                (-1, -1) => CompareEndToStart(first, second, includeHalfOpen) switch
                {
                    -1 => IntervalOverlapping.Before,
                    0 => IntervalOverlapping.Meets,
                    1 => IntervalOverlapping.Overlaps,
                    _ => throw new NotSupportedException(),
                },
                (1, 1) => CompareStartToEnd(first, second, includeHalfOpen) switch
                {
                    -1 => IntervalOverlapping.OverlappedBy,
                    0 => IntervalOverlapping.MetBy,
                    1 => IntervalOverlapping.After,
                    _ => throw new NotSupportedException(),
                },
                (_, _) => throw new NotSupportedException()
            };

        /// <summary>
        /// Compares the start of two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <returns>A value indicating the relative order of the start of the two intervals.</returns>
        [Pure]
        public static int CompareStart<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = first.Start.CompareTo(second.Start);
            return result == 0 ? first.StartInclusive.CompareTo(second.StartInclusive) : result;
        }

        /// <summary>
        /// Compares the end of two intervals.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        [Pure]
        public static int CompareEnd<T>(this Interval<T> first, Interval<T> second)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = first.End.CompareTo(second.End);
            return result == 0 ? first.EndInclusive.CompareTo(second.EndInclusive) : result;
        }

        /// <summary>
        /// Compares the start of the first interval with the end of the second interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        [Pure]
        public static int CompareStartToEnd<T>(this Interval<T> first, Interval<T> second, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = first.Start.CompareTo(second.End);
            var startEndNotTouching = includeHalfOpen
                ? (!first.StartInclusive && !second.EndInclusive)
                : (!first.StartInclusive || !second.EndInclusive);

            return result == 0 && startEndNotTouching ? 1 : result;
        }

        /// <summary>
        /// Compares the end of the first interval with the start of the second interval.
        /// </summary>
        /// <typeparam name="T">The type of the interval endpoints.</typeparam>
        /// <param name="first">The first interval to compare.</param>
        /// <param name="second">The second interval to compare.</param>
        /// <param name="includeHalfOpen">Indicates how to treat half-open endpoints in <see cref="IntervalOverlapping.Meets"/> or <see cref="IntervalOverlapping.MetBy"/> comparison.</param>
        /// <returns>A value indicating the relative order of the end of the two intervals.</returns>
        [Pure]
        public static int CompareEndToStart<T>(this Interval<T> first, Interval<T> second, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = first.End.CompareTo(second.Start);
            var endStartNotTouching = includeHalfOpen
                ? (!first.EndInclusive && !second.StartInclusive)
                : (!first.EndInclusive || !second.StartInclusive);

            return result == 0 && endStartNotTouching ? -1 : result;
        }
    }
}
