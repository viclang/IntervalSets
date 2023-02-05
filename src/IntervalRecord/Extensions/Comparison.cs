using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public static partial class Interval
    {
        //private readonly static Dictionary<ValueTuple<int, int, int, int>, OverlappingState> OverlapStateLookup = new()
        //{
        //    { (-1, -1, -1, -1), OverlappingState.Before },
        //    { (-1, -1, -1, 0), OverlappingState.Meets },
        //    { (-1, -1, -1, 1), OverlappingState.EndInsideOnly },
        //    { (0, -1, -1, 1), OverlappingState.Starts },
        //    { (1, -1, -1, 1), OverlappingState.ContainedBy },
        //    { (1, 0, -1, 1), OverlappingState.Finishes },
        //    { (0, 0, -1, 1), OverlappingState.Equal },
        //    { (-1, 0, -1, 1), OverlappingState.FinishedBy },
        //    { (-1, 1, -1, 1), OverlappingState.Contains },
        //    { (0, 1, -1, 1), OverlappingState.StartedBy },
        //    { (1, 1, -1, 1), OverlappingState.StartInsideOnly },
        //    { (1, 1, 0, 1), OverlappingState.MetBy },
        //    { (1, 1, 1, 1), OverlappingState.After },
        //};

        [Pure]
        public static OverlappingState GetOverlappingState<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (value.Start.CompareTo(other.Start), value.End.CompareTo(other.End)) switch
            {
                (0, 0) => OverlappingState.Equal,
                (0, -1) => OverlappingState.Starts,
                (1, -1) => OverlappingState.ContainedBy,
                (1, 0) => OverlappingState.Finishes,
                (-1, 0) => OverlappingState.FinishedBy,
                (-1, 1) => OverlappingState.Contains,
                (0, 1) => OverlappingState.StartedBy,
                (-1, -1) => CompareEndToStart(value, other, includeHalfOpen) switch
                {
                    -1 => OverlappingState.Before,
                    0 => OverlappingState.Meets,
                    1 => OverlappingState.EndInsideOnly,
                    _ => throw new NotImplementedException(),
                },
                (1, 1) => CompareStartToEnd(value, other, includeHalfOpen) switch
                {
                    1 => OverlappingState.After,
                    0 => OverlappingState.MetBy,
                    -1 => OverlappingState.StartInsideOnly,
                    _ => throw new NotImplementedException(),
                },
                (_, _) => throw new NotImplementedException()
            };

        //[Pure]
        //public static OverlappingState GetOverlappingState<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
        //    where T : struct, IEquatable<T>, IComparable<T>, IComparable
        //{
        //    return OverlapStateLookup
        //        [(
        //            value.Start.CompareTo(other.Start),
        //            value.End.CompareTo(other.End),
        //            CompareStartToEnd(value, other, includeHalfOpen),
        //            CompareEndToStart(value, other, includeHalfOpen)
        //        )];
        //}

        [Pure]
        public static bool IsBefore<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareEndToStart(value, other, includeHalfOpen) == -1;
        }

        [Pure]
        public static bool IsAfter<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return CompareStartToEnd(value, other, includeHalfOpen) == 1;
        }

        [Pure]
        public static bool Meets<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => CompareEndToStart(value, other, includeHalfOpen) == 0;

        [Pure]
        public static bool MetBy<T>(this Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => CompareStartToEnd(value, other, includeHalfOpen) == 0;

        [Pure]
        public static bool Starts<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.Equals(other.Start) && value.End.CompareTo(other.End) == -1;
        }

        [Pure]
        public static bool StartedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.Equals(other.Start) && value.End.CompareTo(other.End) == 1;
        }

        [Pure]
        public static bool StartInsideOnly<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == 1
                && value.End.CompareTo(other.End) == 1
                && value.Start.CompareTo(other.End) == -1;
        }

        [Pure]
        public static bool EndInsideOnly<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == -1
                && value.End.CompareTo(other.End) == -1
                && value.End.CompareTo(other.Start) == 1;
        }

        [Pure]
        public static bool ContainedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == 1 && value.End.CompareTo(other.End) == -1;
        }

        [Pure]
        public static bool Contains<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.Start.CompareTo(other.Start) == -1 && value.End.CompareTo(other.End) == 1;
        }

        [Pure]
        public static bool Contains<T>(this Interval<T> value, T other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.StartInclusive
                ? value.Start.CompareTo(other) <= 0
                : value.Start.CompareTo(other) == -1
                && value.EndInclusive
                ? value.End.CompareTo(other) >= 0
                : value.End.CompareTo(other) == 1;
        }

        [Pure]
        public static bool Finishes<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.End) && value.Start.CompareTo(other.Start) == 1;
        }

        [Pure]
        public static bool FinishedBy<T>(this Interval<T> value, Interval<T> other)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return value.End.Equals(other.End) && value.Start.CompareTo(other.Start) == -1;
        }

        [Pure]
        private static int CompareStartToEnd<T>(Interval<T> value, Interval<T> other, bool includeHalfOpen)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.Start.CompareTo(other.End);
            return result == 0 && StartEndNotTouching(value, other, includeHalfOpen)
                ? 1
                : result;
        }

        [Pure]
        private static int CompareEndToStart<T>(Interval<T> value, Interval<T> other, bool includeHalfOpen)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = value.End.CompareTo(other.Start);
            return result == 0 && EndStartNotTouching(value, other, includeHalfOpen)
                ? -1
                : result;
        }

        [Pure]
        private static bool StartEndNotTouching<T>(Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return includeHalfOpen
                ? (!value.StartInclusive && !other.EndInclusive)
                : (!value.StartInclusive || !other.EndInclusive);
        }

        [Pure]
        private static bool EndStartNotTouching<T>(Interval<T> value, Interval<T> other, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            return includeHalfOpen
                ? (!value.EndInclusive && !other.StartInclusive)
                : (!value.EndInclusive || !other.StartInclusive);
        }
    }
}
