using IntervalRecord.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
    /*
        http://grouper.ieee.org/groups/1788/PositionPapers/ARITHYY.pdf
        http://grouper.ieee.org/groups/1788/PositionPapers/overlapping.pdf
        https://en.wikipedia.org/wiki/Interval_(mathematics)
     */
    public static class IntervalComparison
    {
        private static Dictionary<ValueTuple<int, int, int ,int>, OverlappingState> OverlapStateLookup => new()
        {
            { (-1, -1, -1, -1), OverlappingState.Before },
            { (-1, -1, 0, -1), OverlappingState.Before }, // intfinity case
            { (-1, -1, -1, 0), OverlappingState.Meets },
            { (-1, -1, 0, 0), OverlappingState.Meets }, // Infinite case
            { (-1, -1, -1, 1), OverlappingState.Overlaps },
            { (-1, -1, 0, 1), OverlappingState.Overlaps }, // Infinity case
            { (0, -1, -1, 1), OverlappingState.Starts },
            { (0, -1, 0, 1), OverlappingState.Starts }, //Infinite case
            { (1, -1, -1, 1), OverlappingState.ContainedBy },
            { (1, 0, -1, 1), OverlappingState.Finishes },
            { (1, 0, -1, 0), OverlappingState.Finishes }, //Infinite case
            { (0, 0, -1, 1), OverlappingState.Equal },
            { (0, 0, 0, 0), OverlappingState.Equal }, //Infinite case
            { (-1, 0, -1, 1), OverlappingState.FinishedBy },
            { (-1, 0, 0, 1), OverlappingState.FinishedBy }, //Infinite case
            { (-1, 1, -1, 1), OverlappingState.Contains },
            { (0, 1, -1, 1), OverlappingState.StartedBy },
            { (0, 1, -1, 0), OverlappingState.StartedBy }, //Infinity case
            { (1, 1, -1, 1), OverlappingState.OverlappedBy },
            { (1, 1, -1, 0), OverlappingState.OverlappedBy }, //Infinite case
            { (1, 1, 0, 1), OverlappingState.MetBy },
            { (1, 1, 0, 0), OverlappingState.MetBy }, // Infinite case
            { (1, 1, 1, 1), OverlappingState.After },
            { (1, 1, 1, 0), OverlappingState.After } // Infinity case
        };

        public static OverlappingState GetOverlappingState<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var compareStart = a.Start.CompareTo(b.Start);
            var compareEnd = a.End.CompareTo(b.End);
            var compareStartToEnd = a.Start.CompareTo(b.End);
            var compareEndToStart = a.End.CompareTo(b.Start);

            return OverlapStateLookup[(compareStart, compareEnd, compareStartToEnd, compareEndToStart)];
        }

        public static bool HasInside<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.CompareTo(b.Start) > 1 || a.End.CompareTo(b.End) < 1;
        }


        public static bool IsBefore<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!a.EndInclusive || !b.StartInclusive)
            {
                return a.End.CompareTo(b.Start) <= 0;
            }
            return a.End.CompareTo(b.Start) == -1;
        }

        public static bool IsAfter<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (!a.StartInclusive || !b.EndInclusive)
            {
                return a.Start.CompareTo(b.End) >= 0;
            }
            return a.Start.CompareTo(b.End) == 1;
        }

        public static bool Meets<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.Equals(b.End);
        }

        public static bool MetBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.End.Equals(b.Start);
        }

        public static bool Starts<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.Equals(b.Start) && a.End.CompareTo(b.End) == -1;
        }

        public static bool StartedBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.Equals(b.Start) && a.End.CompareTo(b.End) == 1;
        }

        public static bool ContainedBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.CompareTo(b.Start) == -1 && a.End.CompareTo(b.End) == 1;
        }

        public static bool Contains<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.Start.CompareTo(b.Start) == 1 && a.End.CompareTo(b.End) == -1;
        }

        public static bool Finishes<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.End.Equals(b.End) && a.Start.CompareTo(b.Start) == 1;
        }

        public static bool FinishedBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.End.Equals(b.End) && a.Start.CompareTo(b.Start) == -1;
        }
    }
}
