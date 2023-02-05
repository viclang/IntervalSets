using IntervalRecord.Enums;

namespace IntervalRecord.Extensions
{
    /*
        http://grouper.ieee.org/groups/1788/PositionPapers/ARITHYY.pdf
        http://grouper.ieee.org/groups/1788/PositionPapers/overlapping.pdf
        https://en.wikipedia.org/wiki/Interval_(mathematics)
     */
    public static class IntervalComparison
    {
        private readonly static Dictionary<ValueTuple<int, int, int, int>, OverlappingState> OverlapStateLookup = new()
        {
            { (-1, -1, -1, -1), OverlappingState.Before },
            { (-1, -1, 0, -1), OverlappingState.Before }, // infinity case
            { (-1, -1, -1, 0), OverlappingState.Meets },
            { (-1, -1, 0, 0), OverlappingState.Meets }, // Infinite case
            { (-1, -1, -1, 1), OverlappingState.Overlaps },
            { (-1, -1, 0, 1), OverlappingState.Overlaps }, // Infinity case
            { (0, -1, -1, 1), OverlappingState.Starts },
            { (0, -1, 0, 1), OverlappingState.Starts }, // Infinite case
            { (1, -1, -1, 1), OverlappingState.ContainedBy },
            { (1, 0, -1, 1), OverlappingState.Finishes },
            { (1, 0, -1, 0), OverlappingState.Finishes }, // Infinite case
            { (0, 0, -1, 1), OverlappingState.Equal },
            { (0, 0, 0, 0), OverlappingState.Equal }, // Infinite case
            { (-1, 0, -1, 1), OverlappingState.FinishedBy },
            { (-1, 0, 0, 1), OverlappingState.FinishedBy }, // Infinite case
            { (-1, 1, -1, 1), OverlappingState.Contains },
            { (0, 1, -1, 1), OverlappingState.StartedBy },
            { (0, 1, -1, 0), OverlappingState.StartedBy }, // Infinity case
            { (1, 1, -1, 1), OverlappingState.OverlappedBy },
            { (1, 1, -1, 0), OverlappingState.OverlappedBy }, // Infinite case
            { (1, 1, 0, 1), OverlappingState.MetBy },
            { (1, 1, 0, 0), OverlappingState.MetBy }, // Infinite case
            { (1, 1, 1, 1), OverlappingState.After },
            { (1, 1, 1, 0), OverlappingState.After } // Infinity case
        };

        public static OverlappingState GetOverlappingState<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return OverlapStateLookup
                [(
                    value.Start.CompareTo(other.Start),
                    value.End.CompareTo(other.End),
                    CompareStartToEnd(value, other),
                    CompareEndToStart(value, other)
                )];
        }

        public static OverlappingState GetOverlappingStateExclusive<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return OverlapStateLookup
                [(
                    value.Start.CompareTo(other.Start),
                    value.End.CompareTo(other.End),
                    CompareStartToEndExclusive(value, other),
                    CompareEndToStartExclusive(value, other)
                )];
        }

        public static bool HasInside<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == 1 || value.End.CompareTo(other.End) == 1;
        }

        public static bool IsBefore<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareEndToStart(value, other) == -1;
        }

        public static bool IsAfter<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareStartToEnd(value, other) == 1;
        }

        public static bool IsExclusiveBefore<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareEndToStartExclusive(value, other) == -1;
        }

        public static bool IsExclusiveAfter<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareEndToStartExclusive(value, other) == 1;
        }

        public static bool Meets<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.Start.Equals(other.End);
            return result && !value.EndInclusive && !other.StartInclusive
                ? false
                : result;
        }

        public static bool MeetsClosed<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.Start.Equals(other.End);
            return result && !value.EndInclusive || !other.StartInclusive
                ? false
                : result;
        }

        public static bool MetBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.Start) && value.EndInclusive && other.StartInclusive; 
        }

        public static bool MetByClosed<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.Start) && value.EndInclusive || other.StartInclusive;
        }

        public static bool Starts<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.Equals(other.Start) && value.End.CompareTo(other.End) == -1;
        }

        public static bool StartedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.Equals(other.Start) && value.End.CompareTo(other.End) == 1;
        }

        public static bool ContainedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == -1 && value.End.CompareTo(other.End) == 1;
        }

        public static bool Contains<T>(this Interval<T> value, T other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other) == 1 && value.End.CompareTo(other) == -1;
        }

        public static bool Contains<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == 1 && value.End.CompareTo(other.End) == -1;
        }

        public static bool Finishes<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.End) && value.Start.CompareTo(other.Start) == 1;
        }

        public static bool FinishedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.End) && value.Start.CompareTo(other.Start) == -1;
        }

        private static int CompareStartToEnd<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.Start.CompareTo(other.End);
            return result == 0 && !value.StartInclusive && !other.EndInclusive
                ? 1
                : result;
        }

        private static int CompareEndToStart<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.End.CompareTo(other.Start);
            return result == 0 && !value.EndInclusive && !other.StartInclusive
                ? -1
                : result;
        }

        private static int CompareStartToEndExclusive<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.Start.CompareTo(other.End);
            return result == 0 && !value.StartInclusive || !other.EndInclusive
                ? 1
                : result;
        }

        private static int CompareEndToStartExclusive<T>(Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.End.CompareTo(other.Start);
            return result == 0 && !value.EndInclusive || !other.StartInclusive
                ? -1
                : result;
        }
    }
}
