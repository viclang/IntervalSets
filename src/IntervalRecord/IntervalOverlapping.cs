using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    /// <summary>
    /// Specifies the overlapping relation between two intervals.
    /// </summary>
    public enum IntervalOverlapping : byte
    {
        Before = 0,
        Meets = 1,
        Overlaps = 2,
        Starts = 3,
        ContainedBy = 4,
        Finishes = 5,
        Equal = 6,
        FinishedBy = 7,
        Contains = 8,
        StartedBy = 9,
        OverlappedBy = 10,
        MetBy = 11,
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
