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
            var compareStart = value.Start.CompareTo(other.Start);
            var compareEnd = value.End.CompareTo(other.End);
            var compareStartToEnd = value.Start.CompareTo(other.End);
            var compareEndToStart = value.End.CompareTo(other.Start);

            var result = OverlapStateLookup[(compareStart, compareEnd, compareStartToEnd, compareEndToStart)];
            if (!value.IsClosed() || !other.IsClosed())
            {
                if (result == OverlappingState.Meets)
                {
                    return OverlappingState.Before;
                }
                if (result == OverlappingState.MetBy)
                {
                    return OverlappingState.After;
                }
            }
            return result;
        }

        public static bool HasInside<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) > 1 || value.End.CompareTo(other.End) < 1;
        }


        public static bool IsBefore<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return !value.EndInclusive || !other.StartInclusive
                ? value.End.CompareTo(other.Start) <= 0
                : value.End.CompareTo(other.Start) == -1;
        }

        public static bool IsAfter<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return !value.StartInclusive || !other.EndInclusive
                ? value.Start.CompareTo(other.End) >= 0
                : value.Start.CompareTo(other.End) == 1;
        }

        public static bool Meets<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable => value.Start.Equals(other.End);

        public static bool MetBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.Start);
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
    }
}
