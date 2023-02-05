using System.Diagnostics.Contracts;

namespace IntervalRecord
{
    public enum OverlappingState : byte
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
        [Pure]
        public static OverlappingState GetOverlappingState<T>(this Interval<T> source, Interval<T> value, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
            => (source.CompareStart(value), source.CompareEnd(value)) switch
            {
                (0, 0) => OverlappingState.Equal,
                (0, -1) => OverlappingState.Starts,
                (1, -1) => OverlappingState.ContainedBy,
                (1, 0) => OverlappingState.Finishes,
                (-1, 0) => OverlappingState.FinishedBy,
                (-1, 1) => OverlappingState.Contains,
                (0, 1) => OverlappingState.StartedBy,
                (-1, -1) => CompareEndToStart(source, value, includeHalfOpen) switch
                {
                    -1 => OverlappingState.Before,
                    0 => OverlappingState.Meets,
                    1 => OverlappingState.Overlaps,
                    _ => throw new NotSupportedException(),
                },
                (1, 1) => CompareStartToEnd(source, value, includeHalfOpen) switch
                {
                    -1 => OverlappingState.OverlappedBy,
                    0 => OverlappingState.MetBy,
                    1 => OverlappingState.After,
                    _ => throw new NotSupportedException(),
                },
                (_, _) => throw new NotSupportedException()
            };

        [Pure]
        public static int CompareStart<T>(this Interval<T> source, Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = source.Start.CompareTo(value.Start);
            return result == 0 ? source.StartInclusive.CompareTo(value.StartInclusive) : result;
        }


        [Pure]
        public static int CompareEnd<T>(this Interval<T> source, Interval<T> value)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = source.End.CompareTo(value.End);
            return result == 0 ? source.EndInclusive.CompareTo(value.EndInclusive) : result;
        }

        [Pure]
        public static int CompareStartToEnd<T>(this Interval<T> source, Interval<T> value, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = source.Start.CompareTo(value.End);
            var startEndNotTouching = includeHalfOpen
                ? (!source.StartInclusive && !value.EndInclusive)
                : (!source.StartInclusive || !value.EndInclusive);

            return result == 0 && startEndNotTouching ? 1 : result;
        }

        [Pure]
        public static int CompareEndToStart<T>(this Interval<T> source, Interval<T> value, bool includeHalfOpen = false)
            where T : struct, IEquatable<T>, IComparable<T>, IComparable
        {
            var result = source.End.CompareTo(value.Start);
            var endStartNotTouching = includeHalfOpen
                ? (!source.EndInclusive && !value.StartInclusive)
                : (!source.EndInclusive || !value.StartInclusive);

            return result == 0 && endStartNotTouching ? -1 : result;
        }
    }
}
