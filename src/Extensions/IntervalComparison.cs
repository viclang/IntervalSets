using IntervalRecord.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalRecord
{
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
            var compareStart = a.start.CompareTo(b.start);
            var compareEnd = a.end.CompareTo(b.end);
            var compareStartToEnd = a.start.CompareTo(b.end);
            var compareEndToStart = a.end.CompareTo(b.start);

            return OverlapStateLookup[(compareStart, compareEnd, compareStartToEnd, compareEndToStart)];
        }

        public static bool HasInside<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return (a.CompareStart(b) > 1 || a.CompareEnd(b) < 1);
        }


        public static bool IsBefore<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (a.End is null || b.Start is null)
            {
                return false;
            }
            if (!a.EndInclusive || !b.StartInclusive)
            {
                return a.End!.Value.CompareTo(b.Start!.Value) <= 0;
            }
            return a.End!.Value.CompareTo(b.Start!.Value) == -1;
        }

        public static bool IsAfter<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (a.Start is null || b.End is null)
            {
                return false;
            }
            if (!a.StartInclusive || !b.EndInclusive)
            {
                return a.Start!.Value.CompareTo(b.End!.Value) >= 0;
            }
            return a.Start!.Value.CompareTo(b.End!.Value) == 1;
        }

        public static bool Meets<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if(a.Start is null || b.End is null)
            {
                return false;
            }

            if (a.Start!.Value.Equals(b.End!.Value))
            {
                return true;
            }

            return false;
        }

        public static bool MetBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (a.Start is null || b.End is null)
            {
                return false;
            }

            if (a.End!.Value.Equals(b.Start!.Value))
            {
                return true;
            }

            return false;
        }

        //    if (HasInside(period, test))
        //    {
        //        if (test.Start == period.Start)
        //        {
        //            return PeriodRelation.EnclosingStartTouching;
        //        }
        //        return test.End == period.End ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
        //    }
        public static bool StartedBy<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            if (b.End is null)
            {
                return false;
            }

            if (a.Start!.Value.Equals(b.Start!.Value)
                && a.End!.Value.CompareTo(b.End.Value) == 1)
            {
                return true;
            }

            return false;
        }

        internal static int CompareStart<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.start.CompareTo(b.start);
        }

        internal static int CompareEnd<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.end.CompareTo(b.end);
        }

        internal static int CompareEndToStart<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.end.CompareTo(b.start);
        }

        internal static int CompareStartToEnd<T>(this Interval<T> a, Interval<T> b)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return a.start.CompareTo(b.end);
        }
    }
}
